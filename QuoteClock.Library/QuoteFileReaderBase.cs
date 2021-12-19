using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using QuoteClock.Library.Entities;

namespace QuoteClock.Library
{
    public abstract class QuoteFileReaderBase<T> where T : QuoteElementBase
{
		private string _fileName;
		protected QuoteFileReaderBase(string fileName)
		{
			_fileName = fileName;
		}

		

		public List<T> ReadQuotes()
		{
			var lines = ReadQuotesFromResource(_fileName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			return lines.Select((e, i) => ParseElementFromLine(e, i)).ToList();
		}

		private string ReadQuotesFromResource(string name)
		{
			// Determine path
			var assembliesToLook = new Assembly[] { typeof(QuoteElementTime).Assembly, Assembly.GetExecutingAssembly(), Assembly.GetEntryAssembly() };
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

		protected abstract T ParseElementFromLine(string line, int lineIndex);

		

		protected string GetPartFromSplitted(string[] splitted, int index)
		{
			if(splitted.Length <= index || string.IsNullOrWhiteSpace(splitted[index])) { throw new Exception($"Cannot read element in idx: {index}"); }
			return splitted[index];
		}


	
	
}
}