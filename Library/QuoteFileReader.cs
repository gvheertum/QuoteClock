using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace QuoteClock.Library
{
	public class QuoteElement
	{
		public string TimeString {get;set;}
		public string TimeStringInQuote {get;set;}
		public int Hour { get;set;}
		public int Minute {get;set;}
		public string Quote {get;set;}
		public string Title {get;set;}
		public string Author {get;set;}
	}

	public class QuoteContainer 
	{
		
	}
	public class QuoteFileReader
	{
		private string _fileName;
		public QuoteFileReader(string fileName)
		{
			_fileName = fileName;
		}

		public List<QuoteElement> ReadQuotes()
		{
			var lines = System.IO.File.ReadAllLines(_fileName);
			return lines.Select(GetQuoteElementFromLine).ToList();
		}

		private QuoteElement GetQuoteElementFromLine(string line)
		{
			string[] spl = line.Split(new [] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			
			return new QuoteElement() 
			{ 
				TimeString = GetPartFromSplitted(spl, 0, ""),
				TimeStringInQuote = GetPartFromSplitted(spl, 1, "?"),
				Quote = GetPartFromSplitted(spl, 2, "No-Quote"),
				Title = GetPartFromSplitted(spl, 3, "No Title"),
				Author = GetPartFromSplitted(spl, 4, "No Author"),
			};
		}

		private string GetPartFromSplitted(string[] splitted, int index, string defaultText)
		{
			if(splitted.Length <= index || string.IsNullOrWhiteSpace(splitted[index])) { return defaultText; }
			return splitted[index];
		}
	}
}