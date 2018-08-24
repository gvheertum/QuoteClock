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
	var QuoteUrl = "http://127.0.0.1:5000/api/quote/get/";
	public func GetQuote() -> String
	{
		getJsonFromUrl();
		return "q";
	}
	
	
	//this function is fetching the json from URL
	func getJsonFromUrl(){
		//creating a NSURL
		let url = NSURL(string: QuoteUrl)
		
		//fetching the data from the url
		URLSession.shared.dataTask(with: (url as? URL)!, completionHandler: {(data, response, error) -> Void in
			let y2 = data?.description;
			let jsonRes = try? JSONSerialization.jsonObject(with: data!) as? [String: Any];
			//let q = jsonObj??.quote;
			let harry = Quote(json: jsonRes as! [String : Any]);
			print(harry);
			let  i = 0;
		}).resume();
	}
}


struct Quote
{
	var timeString : String;
	var timeStringInQuote : String;
	var quote : String;
	var raw: String;
	var author : String;
	var title : String;
}

extension Quote
{
	init?(json: [String: Any]) {
		guard let author = json["author"] as? String,
			let timeString = json["timeString"] as? String,
			let raw = json["raw"] as? String,
			let title = json["title"] as? String,
			let quote = json["quote"] as? String,
			let timeStringInQuote = json["timeStringInQuote"] as? String
			else {
				return nil
		}
		self.author = author;
		self.timeString = timeString;
		self.raw = raw;
		self.title = title;
		self.quote = quote;
		self.timeStringInQuote = timeStringInQuote;
	}
}
