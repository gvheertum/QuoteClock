//
//  QuoteRetriever.swift
//  QuoteClock.MacOs
//
//  Created by Gertjan on 21/08/2018.
//  Copyright © 2018 Gertjan. All rights reserved.
//

import Foundation


class QuoteRetriever
{
	
	typealias QuoteChanged = (Quote?) -> Void;
	public var quoteChangedHandler: QuoteChanged? = nil;
	
	var QuoteServiceLocal = "http://127.0.0.1:5000/api/quote/get/";
	var QuoteService = "http://192.168.123.235/QuoteClock/";
	var quoteSvc = "api/quote/get";
	
	public var CurrentQuote : Quote?
	{
		didSet(oldQ)
		{
			quoteChangedHandler?(CurrentQuote);
		}
	}
	
	public func GetQuote()
	{
		getJsonFromUrl();
	}
	
	
	//this function is fetching the json from URL
	func getJsonFromUrl(){
		//creating a NSURL
		var fullUrl = QuoteService + quoteSvc;
		print("Retrieving quote from: " + fullUrl);
		let url = NSURL(string: fullUrl)
		
		
		//fetching the data from the url
		URLSession.shared.dataTask(with: (url as? URL)!, completionHandler: {(data, response, error) -> Void in
			let y2 = data?.description;
			let jsonRes = try? JSONSerialization.jsonObject(with: data!) as? [String: Any];
			//let q = jsonObj??.quote;
			let harry = Quote(json: jsonRes as! [String : Any]);
			self.CurrentQuote = harry;
		}).resume();
	}
}


struct Quote
{
	var timeString : String;
	var timeStringInQuote : String;
	var quote : String;
	var author : String;
	var title : String;
}

extension Quote
{
	init?(json: [String: Any]) {
		guard let author = json["author"] as? String,
			let timeString = json["timeString"] as? String,
			
			let title = json["title"] as? String,
			let quote = json["quote"] as? String,
			let timeStringInQuote = json["timeStringInQuote"] as? String
			else {
				print("The element retrieved from the json is not valid")
				return nil
		}
		self.author = author;
		self.timeString = timeString;
		
		self.title = title;
		self.quote = quote;
		self.timeStringInQuote = timeStringInQuote;
		print("Quote was valid");
	}
	
	//TODO: Add CustomStringConvertible.description as output
}
