﻿using System;
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

                lines.All(delegate (string line)
                {
                    string[] parsedLine = line.Split('|');

                    if (parsedLine.Length == 8)
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
                    }

                    return true;
                });

                string[] parsedData = data.Split('|');

                EventSink.InvokePnpFileParsed();
            }
        }
    }
}
