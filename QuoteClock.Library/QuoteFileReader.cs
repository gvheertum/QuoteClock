using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace QuoteClock.Library
{

	public class QuoteFileReader
	{
		public const string TimeQuotesFileName = "timequotes.txt";
		private string _fileName;
		public QuoteFileReader(string fileName)
		{
			_fileName = fileName;
		}

		

		public List<QuoteElement> ReadQuotes()
		{
			var lines = ReadQuotesFromResource(_fileName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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

		private string ReadQuotesFromResource(string name)
		{
			// Determine path
			var assembliesToLook = new Assembly[] { typeof(QuoteElement).Assembly, Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly() };
			foreach(var assembly in assembliesToLook)
			{
				var resourcePath = assembly?.GetManifestResourceNames()
						.SingleOrDefault(str => str.EndsWith(name));
				
				if(resourcePath == null) { continue; }

				using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						return reader.ReadToEnd();
					}
				}
			}

			// If we did not return by now the file was in none of the calling assemblies and the own assembly
			throw new ArgumentException($"The file {name} was not found in the assemblies");
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