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
			let y = error.debugDescription;
			let jsonObj = try? JSONSerialization.jsonObject(with: data!, options: .allowFragments) as? Quote;
			let q = jsonObj??.quote;
			let  i = 0;
		}).resume()
	}
	
	
}


struct Quote
{
	var timeString : String;
	var timeStringInQuote : String;
	var quote : String;
	var minute : Int;
	var hour : Int;
	var author : String;
	var title : String;
}
