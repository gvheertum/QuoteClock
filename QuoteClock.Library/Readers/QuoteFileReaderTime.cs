using System;
using System.Collections;
using QuoteClock.Library;
using QuoteClock.Library.Entities;

namespace QuoteClock.Library.Reader
{
    public class QuoteFileReaderTime : QuoteFileReaderBase<QuoteElementTime>
	{
		public const string DefaultFileName = "timequotes.txt";
		public QuoteFileReaderTime(string fileName) : base(fileName) {}		

		protected override QuoteElementTime ParseElementFromLine(string line, int lineIndex)
		{
			var qe = new QuoteElementTime() { Raw = line, LineIndex = lineIndex };
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

		private void FixTimeInQuote(QuoteElementTime element)
		{		
			var splitted = (element?.TimeString?? "").Split(new [] {':'}, StringSplitOptions.None);
			if(splitted.Length >= 2)
			{
				if(int.TryParse(splitted[0], out int hour)) { element.Hour = hour; }
				if(int.TryParse(splitted[1], out int minute)) { element.Minute = minute; }
			}
		}
		
	}
}