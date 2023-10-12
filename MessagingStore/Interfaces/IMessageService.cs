using MessagingStore.Models;

namespace MessagingStore.Interfaces
{
    public interface IMessageService
    {
        Task Send(MessageDetail message);
        Task Listen();
    }
}
