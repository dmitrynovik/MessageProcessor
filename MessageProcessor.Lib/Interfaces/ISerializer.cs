using System.IO;

namespace MessageProcessor.Lib.Interfaces
{
    public interface ISerializer
    {
        Stream Serialize(IMessage message);
    }
}
