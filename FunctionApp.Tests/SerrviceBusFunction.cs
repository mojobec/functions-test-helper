using System;
using System.Text;
using FunctionTestHelper;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace FunctionApp.Tests
{
    public class SerrviceBusFunction : FunctionTest
    {
        [Fact]
        public void SerrviceBusFunction_ValidMassage()
        {
            var id = Guid.NewGuid().ToString();
            var jo = new JObject { { "value", id } };
            var msg = new Message(Encoding.UTF8.GetBytes(jo.ToString(Formatting.None)));
            ServiceBusTrigger.Run(msg, log);
        }
    }
}