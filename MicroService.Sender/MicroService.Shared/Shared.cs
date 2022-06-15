using Azure.Messaging.ServiceBus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.Shared
{
    public static class Shared
    {
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
    }
}
