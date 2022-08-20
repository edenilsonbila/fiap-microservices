using AutoMapper;
using Azure.Messaging.ServiceBus;
using GeekBurguer.Ingredientes.DTO;
using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Products.Contract;
using Newtonsoft.Json;

namespace GeekBurguer.Ingredientes.Services
{
    public class ProductChangedConsumerService : IProductChangedConsumer
    {
        private readonly IConfiguration _configuration;
        private const string queueName = "productchanged2";
        static ServiceBusClient client;
        static ServiceBusProcessor processor;
        static List<Task> pendingCompleteTasks = new List<Task>();
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductChangedConsumerService(IConfiguration configuration, IProductRepository productRepository, IMapper mapper)
        {
            _configuration = configuration;
            _productRepository = productRepository;
            _mapper = mapper;
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

            Console.WriteLine("Mensagem Recebida:" + body);

            var productChangedDto = JsonConvert.DeserializeObject<ProductChanged>(body);

            var product = _mapper.Map<Model.Products>(productChangedDto);

            if (productChangedDto.State == ProductState.Added)
                _productRepository.Add(product);
            if (productChangedDto.State == ProductState.Modified)
                _productRepository.Update(product);
            if (productChangedDto.State == ProductState.Deleted)
                _productRepository.Delete(product);

        }

        static Task ExceptionHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task Run()
        {
            var config = _configuration.GetSection("serviceBusProf")
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
