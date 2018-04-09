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
        public PnpFileReader(string filePath)
        {
            string data = "";

            using (StreamReader reader = new StreamReader(filePath, Encoding.Default, true))
            {
                data = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(data))
            {
                string[] lines = data.Split('\n');

                List<PnpPart> parts = new List<PnpPart>();

                lines.Skip(1).All(delegate (string line)
                {
                    line = line.Replace("\r", "").Replace("\n", "");

                    string[] parsedLine = line.Split('|');

                    if (parsedLine.Length == 7)
                    {
                        string refId = parsedLine[0];
                        string packageId = parsedLine[1];
                        double xPosition = double.Parse(parsedLine[2]);
                        double yPosition = double.Parse(parsedLine[3]);
                        string layer = parsedLine[4];
                        double rotation = double.Parse(parsedLine[5]);
                        string value = parsedLine[6];

                        PnpPart part = new PnpPart()
                        {
                            ReferenceID = refId,
                            PackageID = packageId,
                            Position = new Position()
                            {
                                X = xPosition,
                                Y = yPosition
                            },
                            Layer = Layers.Parse(layer),
                            Rotation = rotation,
                            Value = value
                        };

                        parts.Add(part);
                    }

                    return true;
                });

                var smtLayer = Layers.Parse(Settings.Default.SmtLayer);

                var smtParts = parts.Where(q => q.Layer == smtLayer).ToList();
                var dipParts = parts.Where(q => q.Layer != smtLayer).ToList();

                PnpTask contract = new PnpTask();

                var usedPackages = smtParts.GroupBy(q => q.PackageID).Select(q => q.FirstOrDefault().PackageID).ToList();
                var availablePackages = new List<PackageContract>();
                var components = new List<ComponentContract>();

                using (SmdppEntities context = new SmdppEntities())
                {
                    usedPackages.All(delegate (string packageName)
                    {
                        var package = context.Packages.Where(q => q.Name == packageName).Select(q => new PackageContract()
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
                    ComponentContract component = new ComponentContract();
                    component.Layer = part.Layer;
                    component.Name = part.Value;

                    var usedPackage = availablePackages.FirstOrDefault(q => q.Name == part.PackageID);

                    if (usedPackage != null)
                        component.PackageID = usedPackage.ID;

                    component.Position = new Position()
                    {
                        X = part.Position.X,
                        Y = part.Position.Y * -1
                    };

                    component.ReferenceID = part.ReferenceID;
                    component.Rotation = part.Rotation;

                    components.Add(component);

                    return true;
                });

                contract.AvailablePackages = availablePackages;
                contract.Components = components;
                contract.Offset = GetPlacementOffset(components);
                contract.BoardSize = GetBoardDimensions(components);

                EventSink.InvokePnpFileParsed(contract);
            }
        }

        private Position GetPlacementOffset(List<ComponentContract> components)
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

        private Size GetBoardDimensions(List<ComponentContract> components)
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

            return new Size() { Width = (highestX - lowestX) + 40, Height = (highestY - lowestY) + 40};
        }
    }
}
