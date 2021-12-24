using Microsoft.Extensions.Logging;
using QuoteClock.Library.Entities;

namespace QuoteClock.Func
{
    public abstract class QuoteFunctionBase<T, TT> 
        where T : QuoteElementBase 
        where TT : QuoteHandlerBase<T>
    {
        public const string ROUTE_TIME_SPECIFIC = "Quote/Time/{hourString}/{minuteString}";
        public const string ROUTE_TIME_RANDOM = "Quote/Time/Random";
        public const string ROUTE_TIME_NOW = "Quote/Time/Now";
        public const string ROUTE_CURSE = "Quote/Curse/";        
        public const string ROUTE_GENTLESULT = "Quote/Gentlesult/";        

        protected abstract TT GetHandler(ILogger log);
    }
}
