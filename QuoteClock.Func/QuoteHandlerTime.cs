using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteHandlerTime : QuoteHandlerBase<QuoteElementTime> 
    {
        public QuoteHandlerTime(ILogger log) : base(log)
        {
        }

        public QuoteElementTime GetQuote(int hour, int minute){
            _log.LogInformation($"Getting quote for: {hour.ToString().PadLeft(2,'0')}:{minute.ToString().PadLeft(2,'0')}");            
            var qe = (GetQuoteReader() as QuoteContainerTime).GetQuoteForTimeSingle(hour,minute); //Safely assume we are getting the time container here
            return qe ?? GetEmptyResponse(hour,minute);            
        }

        private QuoteElementTime GetEmptyResponse(int hour, int minute)
        {
            return new QuoteElementTime()
            {
                Hour = hour,
                Minute = minute,
                Quote = $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')} already? There is no time for that!",
                Author = "Unknown",
                LineIndex = -1,
                TimeString = $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}"
            };
        }

        protected override QuoteContainerBase<QuoteElementTime> GetQuoteReader() 
        {            
			return new QuoteContainerFactory(new Library.Reader.QuoteFileReaderFactory()).GetQuoteContainerTime();
        }
    }
}
