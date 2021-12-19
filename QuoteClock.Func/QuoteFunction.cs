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
    public class QuoteFunctionTime : QuoteFunctionBase<QuoteElementTime, QuoteHandlerTime>
    {
        [FunctionName($"{nameof(QuoteFunctionTime)}_{nameof(GetSpecific)}")]
        public async Task<IActionResult> GetSpecific(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ROUTE_TIME_SPECIFIC)] HttpRequest req,
            string hourString,
            string minuteString,
            ILogger log)
        {
            log.LogInformation($"Getting quote specific request: {hourString}:{minuteString}");
            if(!int.TryParse(hourString, out int hour) || !int.TryParse(minuteString, out int minute)) 
            {                
                return new OkObjectResult("Invalid request, hour and/or minute part were invalid"); //TODO: This should fail, get correct type
            }
                        
            var q = GetHandler(log).GetQuote(hour, minute);
            return new OkObjectResult(q);
        }

        [FunctionName($"{nameof(QuoteFunctionTime)}_{nameof(GetCurrent)}")]
        public async Task<IActionResult> GetCurrent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = ROUTE_TIME)] HttpRequest req,
            ILogger log)
        {
            DateTime curr = DateTime.UtcNow;
            log.LogInformation($"Get quote for current, current UTC: {curr.ToString()}");
            
            var q = GetHandler(log).GetQuote(curr.Hour, curr.Minute);            
            return new OkObjectResult(q);
        }

        protected override QuoteHandlerTime GetHandler(ILogger log)
        {
            return new QuoteHandlerTime(log);
        }
    }
}
