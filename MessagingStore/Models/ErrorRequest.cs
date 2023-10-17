namespace MessagingStore.Models
{
    public class ErrorRequest
    {
        public RequestInfo? RequestInfo { get; set; }
        public int ErrorCode { get; set; }
    }
}
