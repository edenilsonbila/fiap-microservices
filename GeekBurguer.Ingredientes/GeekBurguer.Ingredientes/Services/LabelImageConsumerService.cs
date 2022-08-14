using Azure.Messaging.ServiceBus;
using GeekBurguer.Ingredientes.Interfaces;

namespace GeekBurguer.Ingredientes.Services
{
    public class LabelImageConsumerService : ILabelImageConsumer
    {
        private readonly IConfiguration _configuration;
        private const string queueName = "labelimageadded";
        static ServiceBusClient client;
        static ServiceBusProcessor processor;
        static List<Task> pendingCompleteTasks = new List<Task>();
        static int count = 0;
        //private readonly IIngredientsService _ingredientsService;

        public LabelImageConsumerService(IConfiguration configuration, IIngredientsService ingredientsService)
        {
            _configuration = configuration;
           // _ingredientsService = ingredientsService;
        }

        async Task ReceiveMessage(ProcessMessageEventArgs args)
        {
            if (args.CancellationToken.IsCancellationRequested || client.IsClosed)
                return;

            Task PendingTask;
            lock (pendingCompleteTasks)
            {
                pendingCompleteTasks.Add(args.CompleteMessageAsync(args.Message));

                PendingTask = pendingCompleteTasks.LastOrDefault();
            }

            await PendingTask;
            pendingCompleteTasks.Remove(PendingTask);

            string body = args.Message.Body.ToString();

            //Finalizar Implementação
            //MargeProductsAndIngredients();

            Console.WriteLine("Mensagem Recebida:" + body);

        }

        static Task ExceptionHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task Run()
        {
            var config = _configuration.GetSection("serviceBus")
                .Get<ServiceBusConfiguration>();
            client = new ServiceBusClient(config.ConnectionString);
            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions()
            {
                AutoCompleteMessages = false,
                ReceiveMode = ServiceBusReceiveMode.PeekLock,

            });
            try
            {
                processor.ProcessMessageAsync += ReceiveMessage;
                processor.ProcessErrorAsync += ExceptionHandler;
                await processor.StartProcessingAsync();
                Console.ReadKey();
                //await processor.StopProcessingAsync();
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
