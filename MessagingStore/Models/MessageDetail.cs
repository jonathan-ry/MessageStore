namespace MessagingStore.Models
{
    public class MessageDetail
    {
        public int Id { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? Message { get; set; }
    }
}
