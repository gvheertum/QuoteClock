using System;
using System.Linq;
using System.Collections.Generic;
using QuoteClock.Library.Reader;

namespace QuoteClock.Library.Entities
{
    public class QuoteContainerTime : QuoteContainerBase<QuoteElementTime>
	{
		public QuoteContainerTime(QuoteFileReaderTime reader) : base(reader) { }

		public IEnumerable<QuoteElementTime> GetQuoteForTime(int hour, int minute)
		{
			return _quotes.Where(q => q.Hour == hour && q.Minute == minute);
		}

		public QuoteElementTime GetQuoteForTimeSingle(int hour, int minute)
		{
			return GetQuoteFromMatches(GetQuoteForTime(hour,minute));
		}

		public QuoteElementTime GetQuoteFromMatches(IEnumerable<QuoteElementTime> elements)
		{
			if(!elements.Any()) { return null; }
			return elements.ElementAt(new Random().Next(0, elements.Count()));
		}
	}
}