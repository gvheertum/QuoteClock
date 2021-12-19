using System;
using System.Linq;
using System.Collections.Generic;
using QuoteClock.Library.Reader;

namespace QuoteClock.Library.Entities
{
    public class QuoteContainerBase<T> where T : QuoteElementBase 
	{
		protected QuoteContainerBase(QuoteFileReaderBase<T> reader) : this(reader?.ReadData()) {}		

		protected List<T> _quotes;
		public QuoteContainerBase(IEnumerable<T> elements)
		{
			_quotes = elements?.ToList() ?? new List<T>();
		}

		public T GetRandom()
		{
			return _quotes[new Random().Next(0, _quotes.Count)];
		}

		public IEnumerable<T> All()
		{
			return _quotes.ToList();
		}

		public IEnumerable<T> Errors()
		{
			return _quotes.Where(q => !string.IsNullOrWhiteSpace(q.Error)).ToList();			
		}
	}
}