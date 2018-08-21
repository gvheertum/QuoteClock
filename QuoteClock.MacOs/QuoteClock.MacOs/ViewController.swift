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
	
	override func viewDidLoad() {
		super.viewDidLoad()

		// Do any additional setup after loading the view.
	}

	override var representedObject: Any? {
		didSet {
		// Update the view, if already loaded.
		}
	}
	
	@IBAction func ButtonQuote_Click(_ sender: Any)
	{
		let qr : QuoteRetriever = QuoteRetriever();
		let q = qr.GetQuote();
		LabelQuote.stringValue = q;
	}
	
	


}

