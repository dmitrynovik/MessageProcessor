namespace MessageProcessor.Lib.Interfaces
{
    public interface ISink
    {
        void Write(SerializerFactory factory, IMessage message);
    }
}
