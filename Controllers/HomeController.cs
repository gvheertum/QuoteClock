using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace QuoteClock.Controllers
{
    public class HomeController : Controller
    {
		private IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }	

        public IActionResult Index()
        {
            return View();
        }

		public string Quotes()
		{
			var quotes = new Library.QuoteFileReader(GetQuoteFileLocation()).ReadQuotes();
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			quotes.Take(100).ToList().ForEach(i => sb.AppendLine(i.Quote));
			return sb.ToString();
		}

		private string GetQuoteFileLocation()
		{
			var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "timeqoutes.txt");
			return file;
		}

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
