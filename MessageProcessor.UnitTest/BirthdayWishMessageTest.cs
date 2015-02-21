using System;
using System.IO;
using System.Linq;
using MessageProcessor.Lib.Interfaces;
using MessageProcessor.Lib.Messages;
using NUnit.Framework;

namespace MessageProcessor.UnitTest
{
    [TestFixture]
    public class BirthdayWishMessageTest : TestBase
    {
        [Test]
        public void Message_Is_Correctly_Serialized()
        {
            var message = CreateMessage();
            using (var stream = SerializerFactory.Get(message.DefaultSerializerType).Serialize(message))
            {
                using (var reader = new StreamReader(stream))
                {
                    var text = reader.ReadToEnd();
                    Console.WriteLine(text);
                    Assert.IsTrue(text.Contains("\"Text\": \"SURPRISE!\""));
                }
            }
        }

        [Test]
        public void When_Message_Is_Processed_File_Is_Written()
        {
            var message = CreateMessage();
            MessageQueue.Enqueue(message);
            MessageQueue.ProcessAll();
            AssertFileWritten(message, "Birthdays");
        }

        private static BirthdayWishMessage CreateMessage()
        {
            var message = new BirthdayWishMessage()
            {
                Name = "John Smith",
                MessageId = Guid.Parse("61B8708C-1EA7-4BC3-83BF-16824FA025E4"),
                Text = "Surprise!",
                BirthDate = new DateTime(1980, 1, 1),
                Gift = "Google Nexus 6",
            };
            return message;
        }
    }
}
