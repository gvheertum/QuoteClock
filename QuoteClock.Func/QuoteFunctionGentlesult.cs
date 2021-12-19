using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteFunctionGentlesult : QuoteFunctionBase<QuoteElementSingular, QuoteHandlerGenltesult>
    {
        protected override QuoteHandlerGenltesult GetHandler(ILogger log)
        {
            return new QuoteHandlerGenltesult(log);
        }
    }
}
