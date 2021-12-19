using QuoteClock.Library;
using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteHandler {
        ILogger log;
        public QuoteHandler(ILogger log) {
            this.log = log;
        }

        public string GetQuote(int hour, int minute){
            log.LogInformation($"Getting quote for: {hour.ToString().PadLeft(2,'0')}:{minute.ToString().PadLeft(2,'0')}");            
            var qe = GetQuoteReader().GetQuoteForTimeSingle(hour,minute);
            return qe?.Raw ?? "No Content";            
        }

        public string GetQuoteRandom()
        {
            log.LogInformation($"Getting random quote");            
            var qe = GetQuoteReader().GetRandom();
            return qe?.Raw ?? "No Content";
        }

        private QuoteContainerTime GetQuoteReader() 
        {            
			return new QuoteContainerFactory(new Library.Reader.QuoteFileReaderFactory()).GetQuoteContainerTime();
        }
    }
}
