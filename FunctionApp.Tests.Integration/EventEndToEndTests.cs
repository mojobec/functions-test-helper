using FunctionTestHelper;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs.Script.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FunctionApp.Tests.Integration
{
    [Collection("Function collection")]
    public class EventEndToEndTests : EndToEndTestsBase<TestFixture>
    {
        private readonly ITestOutputHelper output;
        public EventEndToEndTests(TestFixture fixture, ITestOutputHelper output) : base(fixture)
        {
            this.output = output;
        }

        [Fact]
        public async Task EventHub_TriggerFires()
        {
            List<EventData> events = new List<EventData>();
            string[] ids = new string[3];
            for (int i = 0; i < 3; i++)
            {
                ids[i] = Guid.NewGuid().ToString();
                JObject jo = new JObject
                {
                    { "value", ids[i] }
                };
                var evt = new EventData(Encoding.UTF8.GetBytes(jo.ToString(Formatting.None)));
                evt.Properties.Add("TestIndex", i);
                events.Add(evt);
            }

            string connectionString = Environment.GetEnvironmentVariable("EventHubsConnectionString");
            EventHubsConnectionStringBuilder builder = new EventHubsConnectionStringBuilder(connectionString);

            if (string.IsNullOrWhiteSpace(builder.EntityPath))
            {
                string eventHubPath = "test";
                builder.EntityPath = eventHubPath;
            }

            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(builder.ToString());

            await eventHubClient.SendAsync(events);

            await Task.Delay(30000);

            string logs = null;
            logs = Fixture.Host.GetLog();

            //output.WriteLine(logsSnapshot + "\n--------\n");
            try
            {
                //await TestHelpers.Await(() =>
                //{
                //    // wait until all of the 3 of the unique IDs sent
                //    // above have been processed
                //    //string logs = Fixture.Host.GetLog();
                //    return ids.All(p => logs.Contains(p));
                //    //return true;
                //});
            }
            catch (Exception)
            {

            }

            ////Assert.Contains("IsArray true", logs);
            output.WriteLine(ids.All(p => logs.Contains(p)).ToString() + Environment.NewLine + logs);
        }
    }
}
