using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smdpp;

namespace Smdpp.Logic
{
    public static class LibraryManager
    {
        /// <summary>
        /// Manual parsing has been disabled
        /// </summary>
        /// <param name="filePath"></param>
        public static void ParseAresLibrary(string filePath)
        {
            var data = "";

            using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
            {
                data = reader.ReadToEnd();
            }

            var lines = data.Split('\n').ToList().Skip(11);

            List<Package> detectedPackages = new List<Package>();
            Package packageInstance = null;

            lines.All(delegate (string line)
            {
                /*
                    Object name.......: CHIPFET1206A-03
                    Last modified.....: 06 Haziran 2016 Pazartesi 13:43:55
                    File offset (hex).: 00007D20 (Hex).
                    Checksum (hex)....: F595 (Hex).
                    Size (bytes)......: 1,130
                 * 
                 * 
                 */

                if (packageInstance == null)
                    packageInstance = new Package();

                var splitResult = line.Split(new string[] { ": " }, StringSplitOptions.None );

                if (splitResult.Length == 2)
                {
                    var dataValue = splitResult[1].Replace("\r", "");

                    if (line.StartsWith("Object name"))
                        packageInstance.Name = ParseName(dataValue);
                    /*else if (line.StartsWith("Last modi"))
                        packageInstance.LastModified = ParseLastModified(dataValue);
                    else if (line.StartsWith("File offset"))
                        packageInstance.Offset = ParseOffset(dataValue);
                    else if (line.StartsWith("Checksum"))
                        packageInstance.Checksum = ParseChecksum(dataValue);
                    else if (line.StartsWith("Size"))
                        packageInstance.Size = ParseSize(dataValue);
                    */

                    if (IsPackageReady(packageInstance))
                    {
                        detectedPackages.Add(packageInstance);
                        packageInstance = null;
                    }
                }

                return true;
            });

            if (detectedPackages.Count > 0)
            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    context.Packages.AddRange(detectedPackages);
                    context.SaveChanges();
                }
            }
        }

        private static bool IsPackageReady(Package package)
        {
            /*if (!string.IsNullOrEmpty(package.Name) &&
                package.LastModified.HasValue &&
                package.Offset.HasValue &&
                package.Checksum.HasValue &&
                package.Size.HasValue)
                return true;*/

            return false;
        }

        private static string ParseName(string data)
        {
            return data;
        }

        private static DateTime ParseLastModified(string data)
        {
            var value = DateTime.Parse(data);

            return value;
        }

        private static int ParseOffset(string data)
        {
            data = data.Split(' ')[0];

            return int.Parse(data, NumberStyles.AllowHexSpecifier);
        }

        private static int ParseChecksum(string data)
        {
            data = data.Split(' ')[0];

            return int.Parse(data, NumberStyles.AllowHexSpecifier);
        }

        private static int ParseSize(string data)
        {
            return int.Parse(data.Replace(",", ""));
        }
    }
}
