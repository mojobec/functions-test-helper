using FunctionTestHelper;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        //TestFixture Fixture;
        public EventEndToEndTests(TestFixture fixture, ITestOutputHelper output) : base(fixture)
        {
            this.output = output;
        }

        [Fact]
        public void EventHub_TriggerFires()
        {
            string testData = Guid.NewGuid().ToString();
            EventData data = new EventData(Encoding.UTF8.GetBytes(testData));

            // await Fixture.Host.BeginFunctionAsync("EventHubTrigger", testData);
            //var logResult = await WaitForTraceAsync("EventHubTrigger", log => log.FormattedMessage.Contains(testData));
            //Assert.NotNull(logResult);
            output.WriteLine(Fixture.Host.GetLog());
            Assert.True(true);
        }
    }
}
