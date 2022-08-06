using Azure.Messaging.ServiceBus;
using GeekBurguer.Ingredientes.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace GeekBurger.LabelLoader.Helper
{ 
    internal class Program
    {
        private static readonly ServiceBusConfig _serviceBusConfig = new ConfigHelper()
            .GetInstance()
            .GetSection("ServiceBus")
            .Get<ServiceBusConfig>();
        private static readonly string queueName = "LabelImageAdded";
        static ServiceBusClient _client;
        static ServiceBusSender _sender;

        static void Main(string[] args)
        {
            //IMPLEMENTAR LEITURA DE IMAGENS
            ReadImagesFromFolder();

            //IMPLEMENTAR MONTAGEM DE MENSAGENS
            /*
            new ServiceBusMessage
            {
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new LabelImage())),
                MessageId = Guid.NewGuid().ToString()
            }
            */



            Console.WriteLine(_serviceBusConfig.ConnectionString);
        }

        private static async Task SendMessagesAsync(List<ServiceBusMessage> messages)
        {
            _client = new ServiceBusClient(_serviceBusConfig.ConnectionString);
            _sender = _client.CreateSender(queueName);

            using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();
            foreach (var msg in messages)
            {
                if (!messageBatch.TryAddMessage(msg))
                {
                    throw new Exception($"Falha ao enviar mensagem!");
                }
            }

            try
            {
                var sendTask = _sender.SendMessagesAsync(messageBatch);
                await sendTask;
                CheckCommunicationExceptions(sendTask);
                Console.WriteLine($"Enviado lote de mensagens");
            }
            finally
            {
                await _sender.DisposeAsync();
                await _client.DisposeAsync();
            }

        }

        public static bool CheckCommunicationExceptions(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList()
                .ForEach(innerException =>
                {
                    Console.WriteLine($"Error in SendAsync task: { innerException.Message}.  Details: { innerException.StackTrace} ");

                    if (innerException is ServiceBusException)
                        Console.WriteLine("Connection Problem with Host");
                });

            return false;
        }

        private static void ReadImagesFromFolder()
        {

        }
    }
}





