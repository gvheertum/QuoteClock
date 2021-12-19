using QuoteClock.Library.Reader;

namespace QuoteClock.Library.Entities
{
    public class QuoteContainerGentlesult : QuoteContainerBase<QuoteElementSingular> 
	{
        public QuoteContainerGentlesult(QuoteFileReaderBase<QuoteElementSingular> reader) : base(reader) {}
	}
}