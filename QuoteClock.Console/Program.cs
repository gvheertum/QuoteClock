﻿using System;
using System.Reflection;

namespace QuoteClock.Console
{
    class Program
    {
        static void Main(string[] args)
        {
			ReadResources();
			//TODO: get now if no date provided
			System.Console.WriteLine("Quote generator");
			var reader = new Library.QuoteFileReader(GetQuoteFilePath());
			var quoteContainer = new Library.QuoteContainer(reader);
			while(true)
			{
				RunQuote(quoteContainer);
			}
        }

		private static string GetQuoteFilePath()
		{
			return "timequotes.txt";
		}

		private static void ReadResources()
        {
			var assembly = typeof(Library.QuoteContainer).Assembly;
			var resourceNames = assembly.GetManifestResourceNames();
			foreach(var n in resourceNames)
            {
                System.Console.WriteLine(n);
            }
		}

		private static void RunQuote(Library.QuoteContainer container)
		{
			var qr = new QuoteInputHandling();
			var request = qr.GetInputRequest();
			int? hr = null;
			int? m = null;
			if(request == null) 
			{
				hr = DateTime.Now.Hour;
				m = DateTime.Now.Minute;
			}
			else
			{
				if(!qr.InputIsValid(request)) { System.Console.WriteLine("Invalid input"); return; }
				hr = qr.GetHourFromInput(request);
				m = qr.GetMinuteFromInput(request);
			}
			if(hr == null || m == null) { System.Console.WriteLine("Invalid input"); return; }
			var q = container.GetQuoteForTimeSingle(hr.Value, m.Value);
			if(q == null) { System.Console.WriteLine($"No quote for: {hr.Value}:{m.Value}"); return; }
			ShowQuote(q);
		}

		private static void ShowQuote(Library.QuoteElement quote)
		{
			System.Console.WriteLine("***************************");
			System.Console.WriteLine($"{quote.Hour.ToString().PadLeft(2,'0')}:{quote.Minute.ToString().PadLeft(2,'0')}");
			System.Console.WriteLine($"{quote.TimeStringInQuote}");
			System.Console.WriteLine($"``{quote.Quote}``");
			System.Console.WriteLine($"@{quote.Author}");
			System.Console.WriteLine("***************************");
		}
    }

	public class QuoteInputHandling
	{
		public string GetInputRequest()
		{
			System.Console.WriteLine("Please enter a time as HH:MM, leave empty to get for current time stamp");
			var ts = System.Console.ReadLine();
			return String.IsNullOrWhiteSpace(ts) ? null : ts;
		}

		public bool InputIsValid(string input)
		{
			return !string.IsNullOrWhiteSpace(input) && input.Split(":").Length == 2;
		}

		public int? GetHourFromInput(string input)
		{
			int hr;
			return Int32.TryParse(input.Split(":")[0], out hr) ? hr : (int?)null;
		}

		public int? GetMinuteFromInput(string input)
		{
			int m;
			return Int32.TryParse(input.Split(":")[1], out m) ? m : (int?)null;
		}
	}
}
