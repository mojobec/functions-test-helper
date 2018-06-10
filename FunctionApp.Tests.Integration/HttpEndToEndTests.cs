using FunctionTestHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FunctionApp.Tests.Integration
{
    [Trait("Category", "EndToEnd")]
    [Trait("Group", "HttpTriggerEndToEnd")]
    public class HttpEndToEndTests : EndToEndTestsBase<HttpEndToEndTests.TestFixture>
    {
        public HttpEndToEndTests(TestFixture fixture) : base(fixture)
        { }

        [Fact]
        public async Task HttpTrigger_ValidBody()
        {
            var input = new JObject
            {
                { "name", "Jeff" }
            };
            string key = await Fixture.Host.GetMasterKeyAsync();
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format($"http://localhost/api/HttpTrigger?code={key}")),
                Method = HttpMethod.Post,
                Content = new StringContent(input.ToString())
            };
            HttpResponseMessage response = await Fixture.Host.HttpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string body = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("Hello, Jeff", body);
        }

        
        public class TestFixture : EndToEndTestFixture
        {
            public TestFixture() :
                base(@"..\..\..\..\FunctionApp\bin\Debug\netstandard2.0", "CSharp")
            {
            }

            //protected override IEnumerable<string> GetActiveFunctions() => new[] { "EventHubSender", "EventHubTrigger" };
        }
    }
}
