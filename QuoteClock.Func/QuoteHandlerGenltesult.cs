using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteHandlerGenltesult : QuoteHandlerBase<QuoteElementSingular>
    {
        public QuoteHandlerGenltesult(ILogger log) : base(log) {}

        protected override QuoteContainerBase<QuoteElementSingular> GetQuoteReader() 
        {            
			return new QuoteContainerFactory(new Library.Reader.QuoteFileReaderFactory()).GetQuoteContainerGentlesult();
        }
    }
}
