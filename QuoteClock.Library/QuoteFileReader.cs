using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace QuoteClock.Library
{

	public class QuoteFileReader
	{
		private string _fileName;
		public QuoteFileReader(string fileName)
		{
			_fileName = fileName;
			if(!System.IO.File.Exists(_fileName))
			{
				throw new Exception($"File {_fileName} could not be found");
			}
		}

		public List<QuoteElement> ReadQuotes()
		{
			var lines = System.IO.File.ReadAllLines(_fileName);
			return lines.Select((e, i) => GetQuoteElementFromLine(e, i)).ToList();
		}

		private QuoteElement GetQuoteElementFromLine(string line, int lineIndex)
		{
			var qe = new QuoteElement() { Raw = line, LineIndex = lineIndex };
			try
			{
				string[] spl = line.Split(new [] { '|' }, StringSplitOptions.None);
			
				qe.TimeString = GetPartFromSplitted(spl, 0);
				qe.TimeStringInQuote = GetPartFromSplitted(spl, 1);
				qe.Quote = GetPartFromSplitted(spl, 2);
				qe.Title = GetPartFromSplitted(spl, 3);
				qe.Author = GetPartFromSplitted(spl, 4);
			
				FixTimeInQuote(qe);

				return qe;
			}
			catch(Exception e)
			{
				qe.Error = e.Message;
				return qe;
			}
		}

		private void FixTimeInQuote(QuoteElement element)
		{
			//TODO: Fail safe
			element.Hour = int.Parse(element.TimeString.Split(new [] {':'}, StringSplitOptions.None)[0]);
			element.Minute = int.Parse(element.TimeString.Split(new [] {':'}, StringSplitOptions.None)[1]);
		}

		private string GetPartFromSplitted(string[] splitted, int index)
		{
			if(splitted.Length <= index || string.IsNullOrWhiteSpace(splitted[index])) { throw new Exception($"Cannot read element in idx: {index}"); }
			return splitted[index];
		}
	}
}