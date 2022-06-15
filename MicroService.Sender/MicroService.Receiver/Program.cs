using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Receiver
{
    class Program
    {
        private const string conString = "Endpoint=sb://geekburgeredenilson.servicebus.windows.net/;SharedAccessKeyName=ProductPolicy;SharedAccessKey=83l5/FrUmpONk3tu17z0CIeYuhknNJZgtY1+MSW25W8=";
        private const string queueName = "productchanged";
        static ServiceBusClient client;
        static ServiceBusProcessor processor;
        static List<Task> pendingCompleteTasks = new List<Task>();
        static int count = 0;

        static async Task ReceiveMessage(ProcessMessageEventArgs args)
        {
            if (args.CancellationToken.IsCancellationRequested || client.IsClosed)
                return;

            Task PendingTask;
            lock (pendingCompleteTasks)
            {
                pendingCompleteTasks.Add(args.CompleteMessageAsync(args.Message));
                PendingTask = pendingCompleteTasks.LastOrDefault();
            }

            Console.WriteLine($"task {count++}");
            Console.WriteLine($"calling complete for task {count}");
            await PendingTask;

            Console.WriteLine($"remove task {count} from task queue");
            pendingCompleteTasks.Remove(PendingTask);


            string body = args.Message.Body.ToString();
            Console.WriteLine($"Recebido: {body}");
        }

        static Task ExceptionHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        static async Task Main()
        {
            client = new ServiceBusClient(conString);
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
                Console.WriteLine("Aguardando novas mensagens...");
                Console.ReadKey();
                Console.WriteLine("\nPausando recebimento...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Serviço de recebimento foi parado!");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        private static async Task ReceiveMessagesAsync()
        {
            Console.ReadLine();
            Console.WriteLine($" Request to close async. Pending tasks: { pendingCompleteTasks.Count}");
            await Task.WhenAll(pendingCompleteTasks);
            Console.WriteLine($"All pending tasks were completed");
            var closeTask = client.DisposeAsync();
            await closeTask;
        }

    }
}
