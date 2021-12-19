using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteHandlerTime : QuoteHandlerBase<QuoteElementTime> 
    {
        public QuoteHandlerTime(ILogger log) : base(log)
        {
        }

        public string GetQuote(int hour, int minute){
            _log.LogInformation($"Getting quote for: {hour.ToString().PadLeft(2,'0')}:{minute.ToString().PadLeft(2,'0')}");            
            var qe = (GetQuoteReader() as QuoteContainerTime).GetQuoteForTimeSingle(hour,minute); //Safely assume we are getting the time container here
            return qe?.Raw ?? "No Content";            
        }

        protected override QuoteContainerBase<QuoteElementTime> GetQuoteReader() 
        {            
			return new QuoteContainerFactory(new Library.Reader.QuoteFileReaderFactory()).GetQuoteContainerTime();
        }
    }
}
