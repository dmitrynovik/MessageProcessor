using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using MessageProcessor.Lib.Interfaces;

namespace MessageProcessor.Lib.Serializers
{
    public class XmlSerializer : ISerializer
    {
        public Stream Serialize(IMessage message)
        {
            var stream = new MemoryStream();
            if (message != null)
            {
                var xml = message.Formatted().ToXml();
                var writer = new StreamWriter(stream, Encoding.UTF8);
                writer.WriteLine(xml);
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
            }
            return stream;
        }
    }

    /// <summary>
    /// Needed to serialize anonymous types (built in System.Xml.Serialization.XmlSerializer fails on anonymous types)
    /// </summary>
    public static class XmlTools
    {
        private static readonly Type[] WriteTypes = {
            typeof(string), 
            typeof(DateTime), 
            typeof(decimal), 
            typeof(Guid) };

        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || WriteTypes.Contains(type) || type.IsEnum;
        }

        public static XElement ToXml(this object input)
        {
            return input.ToXml("message");
        }

        public static XElement ToXml(this object input, string element)
        {
            if (input == null)
                return null;

            element = XmlConvert.EncodeName(element);
            var ret = new XElement(element);
            var type = input.GetType();
            var props = type.GetProperties();
            var elements = from prop in props
                           let name = XmlConvert.EncodeName(prop.Name)
                           let val = prop.GetValue(input, null)
                           let value = prop.PropertyType.IsSimpleType()
                                ? new XElement(name, val)
                                : val.ToXml(name)
                           where value != null
                           select value;

            ret.Add(elements);
            return ret;
        }
    }
}
