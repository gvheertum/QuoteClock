using QuoteClock.Library;
using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public abstract class QuoteHandlerBase<T>  where T : QuoteElementBase
    {
        protected ILogger _log;
        public QuoteHandlerBase(ILogger log) {
            this._log = log;
        }
      
        public string GetQuoteRandom()
        {
            _log.LogInformation($"Getting random quote");            
            var qe = GetQuoteReader().GetRandom();
            return qe?.Raw ?? "No Content";
        }

        protected abstract QuoteContainerBase<T> GetQuoteReader();        
    }
}
