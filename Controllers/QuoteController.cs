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

        public IActionResult Index()
        {
            return View();
        }

		public string AllQuotes()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			var quotes = GetQuotes();
			quotes.ToList().ForEach(i => sb.AppendLine(i.Quote));
			return sb.ToString();
		}

		public string Errors()
		{
			var errors = GetQuotes().Where(q => !string.IsNullOrWhiteSpace(q.Error)).ToList();
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			errors.ToList().ForEach(i => sb.AppendLine($"{i.Error} -> {i.Raw}"));
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


	}
}