using System;
using System.Text;
using System.Threading.Tasks;
using FunctionTestHelper;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace FunctionApp.Tests.Integration
{
    public class ServiceBusEndToEndTests : EndToEndTestsBase<TestFixture>
    {
        private readonly ITestOutputHelper _output;
        public ServiceBusEndToEndTests(TestFixture fixture, ITestOutputHelper output)
            : base(fixture)
        {
            _output = output;
        }

        [Fact]
        public async Task ServiceBusQueueTrigger_TriggerFires()
        {
            var id = Guid.NewGuid().ToString();
            var jo = new JObject { { "value", id } };
            var msg = new Message(Encoding.UTF8.GetBytes(jo.ToString(Formatting.None)));
            try
            {
                var cs = AmbientConnectionStringProvider.Instance.GetConnectionString(ConnectionStringNames.ServiceBus);
                await new QueueClient(new ServiceBusConnectionStringBuilder(cs)).SendAsync(msg);
                await WaitForTraceAsync("ServiceBusTrigger", log => log.FormattedMessage.Contains(id));
                _output.WriteLine(Fixture.Host.GetLog());
            }
            catch (Exception ex)
            {
                _output.WriteLine(Fixture.Host.GetLog());
                throw ex;
            }
        }
    }
}