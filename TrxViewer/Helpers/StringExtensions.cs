using System;
using System.IO;
using System.Xml.Serialization;

namespace TrxViewer.Helpers
{
    public static class StringExtensions
    {
        public static bool TryDeserializeFromXml<T>(this string xml, out T result)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using var reader = new StringReader(xml);
                result = (T)serializer.Deserialize(reader);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
    }
}