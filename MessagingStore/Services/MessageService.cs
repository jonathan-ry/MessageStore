using MessagingStore.Interfaces;
using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using MessagingStore.Models;
using System;

namespace MessagingStore.Services
{
    public class MessageService : IMessageService
    {
        string serviceBusConnectionString = "Endpoint=sb://acn-messaging.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=dpABoy7n2yJZvRrMVO7rAofipPqrRAZyg+ASbLRvP20=";
        string queueName = "messagequeue";

        public async Task Send(ErrorResponse message)
        {
            await using var client = new ServiceBusClient(serviceBusConnectionString);

            await using ServiceBusSender sender = client.CreateSender(queueName);

            try
            {
                string messageBody = JsonSerializer.Serialize(message);
                var content = new ServiceBusMessage(messageBody);
                Console.WriteLine($"Sending message: {messageBody}");
                await sender.SendMessageAsync(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {ex.Message}");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
