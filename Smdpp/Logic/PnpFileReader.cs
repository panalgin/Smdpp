using Smdpp.Contracts;
using Smdpp.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public class PnpFileReader
    {
        private List<PnpPart> ExtractParts(string[] lines)
        {
            List<PnpPart> result = new List<PnpPart>();

            lines.Skip(1).All(delegate (string line)
            {
                line = line.Replace("\r", "").Replace("\n", "");

                string[] parsedLine = line.Split('|');

                if (parsedLine.Length == 7)
                {
                    string refId = parsedLine[0];
                    string packageName = parsedLine[1];
                    double xPosition = double.Parse(parsedLine[2]);
                    double yPosition = double.Parse(parsedLine[3]);
                    string layer = parsedLine[4];
                    double rotation = double.Parse(parsedLine[5]);
                    string value = parsedLine[6];

                    PnpPart part = new PnpPart()
                    {
                        ReferenceID = refId,
                        PackageName = packageName,
                        Position = new Position()
                        {
                            X = xPosition,
                            Y = yPosition
                        },
                        Layer = Layers.Parse(layer),
                        Rotation = rotation,
                        Value = value
                    };

                    result.Add(part);
                }

                return true;
            });

            return result;
        }

        public PnpFileReader(string filePath)
        {
            Task.Run(async () =>
            {
                bool result = await this.Parse(filePath);
            });
        }

        private Task<bool> Parse(string filePath)
        {
            string data = "";

            using (StreamReader reader = new StreamReader(filePath, Encoding.Default, true))
            {
                data = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(data))
            {
                string[] lines = data.Split('\n');
                List<PnpPart> parts = ExtractParts(lines);

                Layer smtLayer = Layers.Parse(Settings.Default.SmtLayer);

                List<PnpPart> smtParts = parts.Where(q => q.Layer == smtLayer).ToList();
                List<PnpPart> dipParts = parts.Where(q => q.Layer != smtLayer).ToList();

                PnpJob contract = new PnpJob();
                List<Contracts.Package> availablePackages = new List<Contracts.Package>();

                using (SmdppEntities context = new SmdppEntities())
                {
                    List<string> usedPackages = smtParts.GroupBy(q => q.PackageName).Select(q => q.FirstOrDefault().PackageName).ToList();

                    usedPackages.All(delegate (string packageName)
                    {
                        var package = context.Packages.Where(q => q.Name == packageName).Select(q => new Contracts.Package()
                        {
                            ID = q.ID,
                            Name = q.Name,
                            Data = q.Data
                        }).FirstOrDefault();

                        if (package != null)
                            availablePackages.Add(package);

                        return true;
                    });
                }

                smtParts.All(delegate (PnpPart part)
                {
                    Contracts.Package usedPackage = availablePackages.FirstOrDefault(q => q.Name == part.PackageName);

                    if (usedPackage != null)
                    {
                        part.PackageID = usedPackage.ID;
                    }

                    part.Position.Y *= -1;
                    part.Value = part.Value.ToUpper();

                    return true;
                });

                contract.AvailablePackages = availablePackages;
                contract.Parts = smtParts;
                contract.Offset = GetPlacementOffset(smtParts);
                contract.BoardSize = GetBoardDimensions(smtParts);

                EventSink.InvokePnpFileParsed(contract);

                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }

        private Position GetPlacementOffset(List<PnpPart> components)
        {
            double lowestX = 0;
            double lowestY = 0;

            if (components.Count > 0)
            {
                lowestX = components.Min(q => q.Position.X);
                lowestY = components.Min(q => q.Position.Y);
            }

            return new Position() { X = lowestX, Y = lowestY };
        }

        private Size GetBoardDimensions(List<PnpPart> components)
        {
            double lowestX = 0;
            double lowestY = 0;
            double highestX = 0;
            double highestY = 0;

            if (components.Count > 0)
            {
                lowestX = components.Min(q => q.Position.X);
                lowestY = components.Min(q => q.Position.Y);
                highestX = components.Max(q => q.Position.X);
                highestY = components.Max(q => q.Position.Y);
            }

            return new Size() { Width = (highestX - lowestX) + 40, Height = (highestY - lowestY) + 40 };
        }
    }
}
