using System;

namespace MessageProcessor.Lib.Messages
{
    public class BirthdayWishMessage : MessageBase
    {
        public DateTime BirthDate { get; set; }
        public string Gift { get; set; }

        public override MessageType MessageType { get { return MessageType.BirthdayWish; } }

        public override SerializerType DefaultSerializerType() { return SerializerType.Json; }
        public override string Url() { return "Birthdays"; }
        public override MessageDestination Destination() { return MessageDestination.Disk; }

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
