using System;

namespace MessageProcessor.Lib.Messages
{
    public class BirthdayWishMessage : MessageBase
    {
        public DateTime BirthDate { get; set; }
        public string Gift { get; set; }

        public override SerializerType DefaultSerializerType { get { return SerializerType.Json; } }
        public override string Url { get { return "Birthdays"; } }
        public override MessageDestination Destination { get { return MessageDestination.Disk; } }
        public override MessageType MessageType
        {
            get { return MessageType.BirthdayWish; }
        }

        public override object Formatted()
        {
            return new
            {
                MessageId,
                MessageType,
                Name,
                Text = Text == null ? null : Text.ToUpper(),
                BirthDate,
                Gift,
            };
        }
    }
}
