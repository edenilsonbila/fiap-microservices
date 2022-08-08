using Azure.Messaging.ServiceBus;
using GeekBurguer.Ingredientes.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace GeekBurger.LabelLoader.Helper
{
    internal class Program
    {
        private static ConfigHelper _configHelper = new ConfigHelper();
        private static readonly ServiceBusConfig _serviceBusConfig = _configHelper
            .GetInstance()
            .GetSection("ServiceBus")
            .Get<ServiceBusConfig>();
        private static readonly string queueName = "labelimageadded";
        static ServiceBusClient _client;
        static ServiceBusSender _sender;
        static Thread _readFromFolderThread;

        static void Main(string[] args)
        {
            _readFromFolderThread = new Thread(ReadImagesFromFolder) { IsBackground = true };
            _readFromFolderThread.Start();


            Console.ReadKey();
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

        private static async Task SendMessageAsync(ServiceBusMessage message)
        {
            _client = new ServiceBusClient(_serviceBusConfig.ConnectionString);
            _sender = _client.CreateSender(queueName);

            try
            {
                var sendTask = _sender.SendMessageAsync(message);
                await sendTask;
                CheckCommunicationExceptions(sendTask);
                Console.WriteLine($"Enviando mensagem...");
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

        private static async void ReadImagesFromFolder()
        {
            var labelPath = _configHelper.GetExecutionPath() + _configHelper.GetConfigString("ImageFolder");
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            var visionService = new VisionService();


            if (!Directory.Exists(labelPath))
                Directory.CreateDirectory(labelPath);

            while (true)
            {
                Thread.Sleep(1000);

                var di = new DirectoryInfo(labelPath);

                var images = Directory
               .GetFiles(labelPath)
               .Where(file => imageExtensions.Any(file.ToLower().EndsWith))
               .ToList();

                if (images.Any())
                {
                    foreach (var image in images)
                    {
                        var imageFile = new FileInfo(image);

                        var ingredients = visionService.ReadIngredientsFromImage(imageFile.FullName).Result;

                        var labelImage = new LabelImage
                        {
                            ItemName = "meat",
                            Ingredients = ingredients
                        };

                        var binaryLabelData = BinaryData.FromString(JsonConvert.SerializeObject(labelImage));


                        var messagem = new ServiceBusMessage()
                        {
                            Body = binaryLabelData,
                            MessageId = Guid.NewGuid().ToString()
                        };

                        await SendMessageAsync(messagem);

                        imageFile.Delete();
                    }
                }
            }
        }
    }
}





