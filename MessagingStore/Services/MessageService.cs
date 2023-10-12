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
        string serviceBusConnectionString = "Endpoint=sb://acn-messaging.servicebus.windows.net/;SharedAccessKeyName=API;SharedAccessKey=XUMjRvLWj7af/JuAY5UmxFzAwtHL1U/p1+ASbCqdAZ8=;EntityPath=messagequeue";
        string queueName = "messagequeue";
  
        public async Task Listen()
        {
            var client = new ServiceBusClient(serviceBusConnectionString);

            var processorOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            await using ServiceBusProcessor processor = client.CreateProcessor(queueName, processorOptions);

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;


            await processor.StartProcessingAsync();

            await processor.CloseAsync();
        }

        // handle received messages
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task Send(MessageDetail message)
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
