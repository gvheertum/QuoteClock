using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace QuoteClock.Func
{
    public static class QuoteFunction
    {
        [FunctionName("Quote_Time")]
        public static async Task<IActionResult> GetQuoteTime(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Quote/{hourString}/{minuteString}")] HttpRequest req,
            string hourString,
            string minuteString,
            ILogger log)
        {
            log.LogInformation($"Getting quote specific request: {hourString}:{minuteString}");
            if(!int.TryParse(hourString, out int hour) || !int.TryParse(minuteString, out int minute)) 
            {                
                return new OkObjectResult("Invalid request, hour and/or minute part were invalid"); //TODO: This should fail, get correct type
            }
                        
            var q = new QuoteHandler(log).GetQuote(hour, minute);
            return new OkObjectResult(q);
        }

        [FunctionName("Quote_Now")]
        public static async Task<IActionResult> GetQuoteForCurrentTime(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Quote/")] HttpRequest req,
            ILogger log)
        {
            DateTime curr = DateTime.UtcNow;
            log.LogInformation($"Get quote for current, current UTC: {curr.ToString()}");
            
            var q = new QuoteHandler(log).GetQuote(curr.Hour, curr.Minute);            
            return new OkObjectResult(q);
        }
    }
}
