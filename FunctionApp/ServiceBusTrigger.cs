using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class ServiceBusTrigger
    {
        [FunctionName("ServiceBusTrigger")]
        public static void Run([ServiceBusTrigger("test")] Message message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus trigger function processed a message: {Encoding.UTF8.GetString(message.Body)}");
        }
    }
}