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
      
        public T GetQuoteRandom()
        {
            _log.LogInformation($"Getting random quote");            
            var qe = GetQuoteReader().GetRandom();
            return qe;
        }

        protected abstract QuoteContainerBase<T> GetQuoteReader();        
    }
}
