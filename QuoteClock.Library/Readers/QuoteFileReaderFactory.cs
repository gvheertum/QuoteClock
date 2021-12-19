namespace QuoteClock.Library.Reader
{
    public class QuoteFileReaderFactory
    {
        public QuoteFileReaderCurse GetQuoteFileReaderCurse(string altPath = null) 
        {
            return new QuoteFileReaderCurse(altPath ?? QuoteFileReaderCurse.DefaultFileName);
        }   
        public QuoteFileReaderGentlesult GetQuoteFileReaderGentlesult(string altPath = null) 
        {
            return new QuoteFileReaderGentlesult(altPath ?? QuoteFileReaderGentlesult.DefaultFileName);
        }   
        public QuoteFileReaderTime GetQuoteFileReaderTime(string altPath = null) 
        {
            return new QuoteFileReaderTime(altPath ?? QuoteFileReaderTime.DefaultFileName);
        }   
    }
}