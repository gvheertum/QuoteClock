namespace QuoteClock.Library.Reader
{
    public class QuoteFileReaderGentlesult : QuoteFileReaderSingular
    {
		public const string DefaultFileName = "oudevloekwoorden.txt";
        public QuoteFileReaderGentlesult(string fileName) : base(fileName) {}         
    }
}