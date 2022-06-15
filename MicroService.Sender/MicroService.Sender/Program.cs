using Azure.Messaging.ServiceBus;
using System;
using System.Linq;
using System.Threading.Tasks;
using MicroService.Shared;

namespace MicroService.Sender
{
    public class Program
    {
        private const string conString = "Endpoint=sb://geekburgeredenilson.servicebus.windows.net/;SharedAccessKeyName=ProductPolicy;SharedAccessKey=83l5/FrUmpONk3tu17z0CIeYuhknNJZgtY1+MSW25W8=";
        private const string queueName = "productchanged";//Nome da fila
        static ServiceBusClient client;
        static ServiceBusSender sender;
        public static async Task Main(string[] args)
        {
            await SendMessagesAsync();
        }

        private static async Task SendMessagesAsync()
        {
            client = new ServiceBusClient(conString);
            sender = client.CreateSender(queueName);

            var messages = "Hi,Hello,Hey,How are you,Be Welcome"
            .Split(",").ToList();

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            foreach (var msg in messages)
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(msg)))
                {
                    throw new Exception($"Falha ao enviar mensagem!");
                }
            }

            try
            {
                var sendTask = sender.SendMessagesAsync(messageBatch);
                await sendTask;
                Shared.Shared.CheckCommunicationExceptions(sendTask);
                Console.WriteLine($"Enviado lote de mensagens");
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

        }
    }
}
