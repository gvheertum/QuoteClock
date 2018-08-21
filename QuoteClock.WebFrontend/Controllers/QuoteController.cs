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
        public JsonResult Get(int? hour, int? minute)
		{
			if(hour == null && minute == null)
			{
				hour = DateTime.Now.Hour;
				minute = DateTime.Now.Hour;
			}
			return Index(hour ?? 0, minute ?? 0);
		}

		

		//IActionResult
        public JsonResult Index(int? hour, int? minute)
        {
			var qc = GetQuoteContainer();
            var quote = qc.GetQuoteForTimeSingle(hour.Value, minute.Value);
			return Json(quote != null ? quote : GetEmptyQuote(hour.Value,minute.Value));
        }

		private QuoteElement GetEmptyQuote(int hour, int minute)
		{
			return new QuoteElement()
			{
				Hour = hour,
				Minute = minute,
				Title = "Eigen werk",
				Author = "Gertjan",
				Quote = "Look at the clock! There is not time for quotes!",
				TimeString = hour.ToString().PadLeft(2, '0')  + ":" + minute.ToString().PadLeft(2, '0'),
				TimeStringInQuote = "No time!"
			};
		}

        [HttpGet("api/quote/random/")]
		public JsonResult Random()
		{
			var qc = GetQuoteContainer();
			return Json(qc.GetRandom());
		}

		// private string FormatQuote(QuoteElement q)
		// {
		// 	return $@"{q.TimeString}-{q.TimeStringInQuote}\r\n{q.Quote}\r\n@{q.Author}";
		// }

		private Library.QuoteContainer GetQuoteContainer()
		{
			var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "timequotes.txt");
			return new Library.QuoteContainer(new Library.QuoteFileReader(file));
		}


	}
}