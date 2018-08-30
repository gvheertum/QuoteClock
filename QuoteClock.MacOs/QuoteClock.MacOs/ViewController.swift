//
//  ViewController.swift
//  QuoteClock.MacOs
//
//  Created by Gertjan on 21/08/2018.
//  Copyright © 2018 Gertjan. All rights reserved.
//

import Cocoa

class ViewController: NSViewController {


	@IBOutlet weak var LabelQuote: NSTextField!
	@IBOutlet weak var LabelAuthor: NSTextField!
	@IBOutlet weak var LabelQuoteTime: NSTextField!
	@IBOutlet weak var LabelTitle: NSTextField!
	@IBOutlet weak var LabelRealTime: NSTextField!
	
	override func viewDidLoad() {
		super.viewDidLoad()
	
		// Do any additional setup after loading the view.
		loadQuote();
		startTimer();
	}

	override var representedObject: Any? {
		didSet {
		// Update the view, if already loaded.
		}
	}
	
	@IBAction func ButtonQuote_Click(_ sender: Any)
	{
		loadQuote();
	}
	
	@objc public func loadQuote()
	{
		print("called loadQuote");
		let qr : QuoteRetriever = QuoteRetriever();
		
		//Listen to the quote change
		qr.quoteChangedHandler = { nw in self.QuoteChanged(quote: nw); return; }
		
		//Run forrest run!
		qr.GetQuote();
	}
	
	func startTimer()
	{
		var t = Timer.scheduledTimer(timeInterval: 10, target: self, selector: #selector(loadQuote), userInfo: nil, repeats: true);
	}
	
	func QuoteChanged(quote: Quote?)
	{
		print("Called Quote change on client");
		guard let q : Quote = quote
		else
		{
			print("Quote was nil, ignore change event");
			return;
		}
		//Tell the gui (on the mainthread) to update the values
		DispatchQueue.main.async {
			self.updateQuoteInGUI(q: q);
		}
	}
	
	func updateQuoteInGUI(q: Quote)
	{
		self.LabelQuote.stringValue = q.quote;
		self.LabelAuthor.stringValue = q.author;
		self.LabelTitle.stringValue = q.title;
		self.LabelRealTime.stringValue = q.timeString;
		self.LabelQuoteTime.stringValue = q.timeStringInQuote;
	}
	


}

