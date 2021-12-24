using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteFunctionCurse : QuoteFunctionBase<QuoteElementSingular, QuoteHandlerCurse>
    {

        [FunctionName($"{nameof(QuoteFunctionCurse)}_{nameof(GetRandom)}")]
        public async Task<IActionResult> GetRandom(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ROUTE_CURSE)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Getting Curse");
            
            var q = GetHandler(log).GetQuoteRandom();
            return new OkObjectResult(q);
        }

        protected override QuoteHandlerCurse GetHandler(ILogger log)
        {
            return new QuoteHandlerCurse(log);
        }
    }
}
