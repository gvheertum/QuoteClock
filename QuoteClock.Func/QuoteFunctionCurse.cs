using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public class QuoteFunctionCurse : QuoteFunctionBase<QuoteElementSingular, QuoteHandlerCurse>
    {
        protected override QuoteHandlerCurse GetHandler(ILogger log)
        {
            return new QuoteHandlerCurse(log);
        }
    }
}
