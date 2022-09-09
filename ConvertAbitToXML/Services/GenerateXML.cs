using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConvertAbitToXML
{
    public class GenerateXML
    {
        #region Template XML


        public static void SerializeObject<T>(List<T> obj, Encoding enc , string fileName)
        {
            //using FileStream fileStream = new(fileName, FileMode.Create);
            XmlWriterSettings xmlWriterSettings = new()
            {
                CloseOutput = false,
                Encoding = enc,
                OmitXmlDeclaration = false,
                Indent = true
            };
            using XmlWriter xw = XmlWriter.Create(fileName, xmlWriterSettings);
            XmlSerializer ser = new(typeof(List<T>));
            ser.Serialize(xw, obj);
        }

        // Template для сериализации в XML
        public static void SerializeToXml<T>(List<T> obj, string fileName)
        {
            using var fileStream = new FileStream(fileName, FileMode.Create);
            var ser = new XmlSerializer(typeof(List<T>));
            ser.Serialize(fileStream, obj);
        }
        // Template для десириализации в XML
        public static T DeserializeFromXml<T>(string xml)
        {
            var ser = new XmlSerializer(typeof(T));
            using var tr = new StringReader(xml);
            var result = (T)ser.Deserialize(tr);
            return result;
        }

        #endregion
    }
}
