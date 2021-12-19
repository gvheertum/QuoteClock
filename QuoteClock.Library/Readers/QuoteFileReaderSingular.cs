using QuoteClock.Library.Entities;

namespace QuoteClock.Library.Reader
{
    public abstract class QuoteFileReaderSingular : QuoteFileReaderBase<QuoteElementSingular>
    {
        protected QuoteFileReaderSingular(string fileName) : base(fileName)
        {
        }      

        protected override QuoteElementSingular ParseElementFromLine(string line, int lineIndex)
        {
            return new QuoteElementSingular() 
			{
				LineIndex = lineIndex,
				Raw = line,
				Element = line,
			};
        }
    }
}