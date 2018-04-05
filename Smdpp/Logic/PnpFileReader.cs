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

                var definitions = new List<ComponentPlacementContract>();

                ComponentPlacementContract contract = new ComponentPlacementContract();
                
                var packageNames = smtParts.GroupBy(q => q.)
                smtParts.All(delegate (PnpPart part)
                {
                    
                    if (definitions.FirstOrDefault(q => q.ReferenceID == part.ReferenceID)
                    return true;
                });
                

                PnpTask task = new PnpTask()
                {
                    SmtParts = smtParts,
                    DipParts = dipParts,
                    Definitions = definitions,

                };

                EventSink.InvokePnpFileParsed(task);
            }
        }
    }
}
