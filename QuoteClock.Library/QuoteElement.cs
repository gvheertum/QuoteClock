namespace QuoteClock.Library
{
	public class QuoteElement
	{
		public string Raw {get;set;}
		public int LineIndex {get;set;}
		public string TimeString {get;set;}
		public string TimeStringInQuote {get;set;}
		public int Hour { get;set;}
		public int Minute {get;set;}
		public string Quote {get;set;}
		public string Title {get;set;}
		public string Author {get;set;}
		public string Error {get;set;}
	}
}