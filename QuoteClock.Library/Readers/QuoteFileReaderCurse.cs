namespace QuoteClock.Library.Reader
{
    public class QuoteFileReaderCurse : QuoteFileReaderSingular
    {
		public const string DefaultFileName = "oudeuitdrukkingen.txt";
        public QuoteFileReaderCurse(string fileName) : base(fileName) {}
    }
}