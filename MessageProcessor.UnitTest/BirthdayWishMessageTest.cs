using System;
using System.IO;
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
            var text = CreateAndSerializeMessage();
            Assert.IsTrue(text.Contains("\"Text\": \"SURPRISE!\""));
        }

        [Test]
        public void When_Message_Is_Processed_File_Is_Written()
        {
            var message = ProcessMessage();
            MessageQueue.Enqueue(message);
            MessageQueue.ProcessAll();
            AssertMessageWrittenToFile(message, "Birthdays");
        }

        protected override IMessage CreateMessage()
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
