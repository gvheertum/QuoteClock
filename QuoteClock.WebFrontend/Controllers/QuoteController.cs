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
			var qc = GetQuoteContainer();
            var quote = qc.GetQuoteForTimeSingle(hour.Value, minute.Value);
			return quote != null ? FormatQuote(quote) : "No quote for time";
        }

        [HttpGet("api/quote/random/")]
		public string Random()
		{
			var qc = GetQuoteContainer();
			return FormatQuote(qc.GetRandom());
		}

		private string FormatQuote(QuoteElement q)
		{
			return $@"{q.TimeString}-{q.TimeStringInQuote}\r\n{q.Quote}\r\n@{q.Author}";
		}

		private Library.QuoteContainer GetQuoteContainer()
		{
			var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "timequotes.txt");
			return new Library.QuoteContainer(new Library.QuoteFileReader(file));
		}


	}
}