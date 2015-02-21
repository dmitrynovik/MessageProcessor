using System;
using MessageProcessor.Lib.Interfaces;
using MessageProcessor.Lib.Serializers;

namespace MessageProcessor.Lib
{
    public class SerializerFactory
    {
        public ISerializer Get(SerializerType type)
        {
            switch (type)
            {
                case SerializerType.Json:
                    return new JsonSerializer();
                case SerializerType.Xml:
                    return new XmlSerializer();
                default:
                    throw new ArgumentException(string.Format("Unknown serializer type: {0}", type), "type");
            }
        }
    }
}
