using QuoteClock.Library.Reader;

namespace QuoteClock.Library.Entities
{
    public class QuoteContainerFactory
	{
        private readonly QuoteFileReaderFactory _readerFactory;

        public QuoteContainerFactory(QuoteFileReaderFactory readerFactory) 
		{
			_readerFactory = readerFactory;
		}
		public QuoteContainerTime GetQuoteContainerTime(string pathOverride = null) 
		{
			return new QuoteContainerTime(_readerFactory.GetQuoteFileReaderTime(pathOverride));
		} 	
		public QuoteContainerGentlesult GetQuoteContainerGentlesult(string pathOverride = null) 
		{
			return new QuoteContainerGentlesult(_readerFactory.GetQuoteFileReaderGentlesult(pathOverride));
		} 	
		public QuoteContainerCurse GetQuoteContainerCurse(string pathOverride = null) 
		{
			return new QuoteContainerCurse(_readerFactory.GetQuoteFileReaderCurse(pathOverride));
		}
	}
}