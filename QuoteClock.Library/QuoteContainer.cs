using System;
using System.Linq;
using System.Collections.Generic;

namespace QuoteClock.Library
{
	public class QuoteContainer 
	{
		public QuoteContainer(QuoteFileReader reader) : this(reader.ReadQuotes())
		{

		}

		private List<QuoteElement> _quotes;
		public QuoteContainer(IEnumerable<QuoteElement> elements)
		{
			_quotes = elements?.ToList() ?? new List<QuoteElement>();
		}


		public IEnumerable<QuoteElement> GetQuoteForTime(int hour, int minute)
		{
			return _quotes.Where(q => q.Hour == hour && q.Minute == minute);
		}

		public QuoteElement GetQuoteForTimeSingle(int hour, int minute)
		{
			return GetQuoteFromMatches(GetQuoteForTime(hour,minute));
		}

		public QuoteElement GetQuoteFromMatches(IEnumerable<QuoteElement> elements)
		{
			if(!elements.Any()) { return null; }
			return elements.ElementAt(new Random().Next(0, elements.Count()));
		}

		public QuoteElement GetRandom()
		{
			return _quotes[new Random().Next(0, _quotes.Count)];
		}

		public IEnumerable<QuoteElement> All()
		{
			return _quotes.ToList();
		}

		public IEnumerable<QuoteElement> Errors()
		{
			var errors = _quotes.Where(q => !string.IsNullOrWhiteSpace(q.Error)).ToList();
			if(!errors.Any()) { return new List<QuoteElement>(); }

			return errors;
		}
		

	}
}