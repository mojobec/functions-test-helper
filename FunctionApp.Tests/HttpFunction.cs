using FunctionTestHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using Xunit;

namespace FunctionApp.Tests
{
    public class HttpFunction : FunctionTestHelper.FunctionTest
    {
        [Fact]
        public void HttpTrigger_ValidInput()
        {
            var query = new Dictionary<String, StringValues>();
            query.TryAdd("name", "Jeff");

            var result = HttpTrigger.Run(
                req: HttpTestHelpers.CreateHttpRequest("POST", "http://localhost", body: new { name = "Jeff"}), 
                log: log);

            var resultObject = (OkObjectResult)result;
            Assert.Equal("Hello, Jeff", resultObject.Value);
        }
    }
}
