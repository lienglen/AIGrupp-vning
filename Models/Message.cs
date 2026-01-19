using AIGruppÖvning.Enums;

namespace AIGruppÖvning.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public UserType Sender { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }       
    }
}
