namespace MessagingStore.Models
{
    public class ErrorResponse
    {
        public Guid ErrorId { get; set; }
        public DateTime Timestamp { get; set; }
        public int HttpStatusCode { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Location { get; set; }
        public RequestInfo? RequestInfo { get; set; }
    }
}
