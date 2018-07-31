using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using QuoteClock.Library;

namespace QuoteClock.Controllers
{
    public class QuoteController : Controller
    {
		private IHostingEnvironment _env;
        public QuoteController(IHostingEnvironment env)
        {
            _env = env;
        }	

        [HttpGet("api/quote/get/{hour}/{minute}")]
        [HttpGet("api/quote/get/")]
        public string Get(int? hour, int? minute)
		{
			if(hour == null && minute == null)
			{
				hour = DateTime.Now.Hour;
				minute = DateTime.Now.Hour;
			}
			return Index(hour ?? 0, minute ?? 0);
		}

		//IActionResult
        public string Index(int? hour, int? minute)
        {
            var quote = GetQuoteFromMatches(GetQuoteForTime(hour.Value,minute.Value));
			return quote != null ? quote.Quote : "No quote for time";
        }

		public string Random()
		{
			return "Not implemented yet";
		}


		public string All()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			var quotes = GetQuotes();
			quotes.ToList().ForEach(i => sb.AppendLine($"{i.TimeString} ({i.TimeStringInQuote}) -> {i.Quote} ({i.Author} @ {i.Title})"));
			return sb.ToString();
		}

		public string Errors()
		{
			var errors = GetQuotes().Where(q => !string.IsNullOrWhiteSpace(q.Error)).ToList();
			if(!errors.Any()) { return "No errors, file seems clean"; }

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.AppendLine($"Found {errors.Count()} errors:");
			errors.ToList().ForEach(i => sb.AppendLine($"Line: {i.LineIndex} !{i.Error} -> {i.Raw}"));
			return sb.ToString();
		}

		private IEnumerable<QuoteElement> GetQuotes()
		{
			return new Library.QuoteFileReader(GetQuoteFileLocation()).ReadQuotes();
		}

		private string GetQuoteFileLocation()
		{
			var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "timeqoutes.txt");
			return file;
		}


		private IEnumerable<QuoteElement> GetQuoteForTime(int hour, int minute)
		{
			return GetQuotes().Where(q => q.Hour == hour && q.Minute == minute);
		}

		private QuoteElement GetQuoteFromMatches(IEnumerable<QuoteElement> elements)
		{
			if(!elements.Any()) { return null; }
			return elements.ElementAt(new Random().Next(0, elements.Count()));
		}
	}
}