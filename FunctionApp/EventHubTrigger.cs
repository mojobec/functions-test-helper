using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.Text;

namespace FunctionApp
{
    public static class EventHubTrigger
    {
        [FunctionName("EventHubTrigger")]
        public static void Run([EventHubTrigger("test", Connection = "EventHubsConnectionString")]EventData[] eventDataArray, TraceWriter log)
        {
            foreach(var e in eventDataArray)
            {
                log.Info($"C# Event Hub trigger function processed a message: {Encoding.UTF8.GetString(e.Body.Array)}");
            }
        }
    }
}
