using System;
using System.Linq;
using System.Collections.Generic;

namespace QuoteClock.Library.Entities
{
	public class QuoteContainerBase<T> where T : QuoteElementBase 
	{

	}
	public class QuoteContainerCurse : QuoteContainerBase<QuoteElementSingular> 
	{

	}

	public class QuoteContainerGentlesult : QuoteContainerBase<QuoteElementSingular> 
	{

	}
	public class QuoteContainerTime : QuoteContainerBase<QuoteElementTime>
	{
		public QuoteContainerTime(QuoteFileReaderTime reader) : this(reader.ReadQuotes())
		{

		}

		private List<QuoteElementTime> _quotes;
		public QuoteContainerTime(IEnumerable<QuoteElementTime> elements)
		{
			_quotes = elements?.ToList() ?? new List<QuoteElementTime>();
		}


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

		public QuoteElementTime GetRandom()
		{
			return _quotes[new Random().Next(0, _quotes.Count)];
		}

		public IEnumerable<QuoteElementTime> All()
		{
			return _quotes.ToList();
		}

		public IEnumerable<QuoteElementTime> Errors()
		{
			var errors = _quotes.Where(q => !string.IsNullOrWhiteSpace(q.Error)).ToList();
			if(!errors.Any()) { return new List<QuoteElementTime>(); }

			return errors;
		}
		

	}
}