using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BUSK.Charting.Maps;

namespace BUSK.Charting.WPF.Maps
{
    internal static class MapResolver
    {
        public static LvcMap Get(string file)
        {
            //file = Path.Combine(Directory.GetCurrentDirectory(), file);

            if (!File.Exists(file))
            {
                throw new FileNotFoundException(String.Format("This file {0} was not found.", file));
            }

            var svgMap = new LvcMap
            {
                DesiredHeight = 600,
                DesiredWidth = 800,
                Data = new List<MapData>()
            };

            using (var reader = XmlReader.Create(file, new XmlReaderSettings {IgnoreComments = true}))
            {
                while (reader.Read())
                {
                    if (reader.Name == "Height") svgMap.DesiredHeight = double.Parse(reader.ReadInnerXml());
                    if (reader.Name == "Width") svgMap.DesiredWidth = double.Parse(reader.ReadInnerXml());
                    if (reader.Name == "MapShape")
                    {
                        var p = new MapData
                        {
                            LvcMap = svgMap
                        };
                        reader.Read();
                        while (reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (reader.NodeType != XmlNodeType.Element) reader.Read();
                            if (reader.Name == "Id") p.Id = reader.ReadInnerXml();
                            if (reader.Name == "Name") p.Name = reader.ReadInnerXml();
                            if (reader.Name == "Path") p.Data = reader.ReadInnerXml();
                        }
                        svgMap.Data.Add(p);
                    }
                }
            }
            return svgMap;
        }
    }
}
