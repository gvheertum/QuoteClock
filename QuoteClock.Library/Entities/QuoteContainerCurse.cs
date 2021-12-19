using QuoteClock.Library.Reader;

namespace QuoteClock.Library.Entities
{
    public class QuoteContainerCurse : QuoteContainerBase<QuoteElementSingular>
    {
        public QuoteContainerCurse(QuoteFileReaderBase<QuoteElementSingular> reader) : base(reader) {}
    }
}