import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'quoter',
    templateUrl: './quoter.component.html',
    styleUrls: ['./quoter.component.css']
})
export class QuoterComponent 
{
	private baseUrl : string;
	private http : Http;
	private quote: IQuote | null;
	private minute: number | null;
	private hour: number | null;
	constructor(http: Http, @Inject('BASE_URL') baseUrl: string) 
	{
		this.http = http;
		this.baseUrl = baseUrl;
		this.quote = null;
		this.hour = null;
		this.minute = null;
		this.getCurrentTimeQuote();
		setInterval(() => this.tick(), 10000);
	}
		
	public getCurrentTimeQuote()
	{
		this.hour = new Date().getHours();
		this.minute = new Date().getMinutes();
		this.http.get(this.baseUrl + 'api/quote/get/' + this.hour + "/" + this.minute) .subscribe(result => {
			var q = result.json() as IQuote;
			this.quote = q;
		}, error => console.error(error));
	}

	public tick()
	{
		var newHour = new Date().getHours();
		var newMinute = new Date().getMinutes();
		if(this.hour != newHour || this.minute != newMinute)
		{
			this.getCurrentTimeQuote();
		}
	}

	public getRandomQuote()
	{
		this.http.get(this.baseUrl + 'api/quote/random').subscribe(result => {
			var q = result.json() as IQuote;
			this.quote = q;
		}, error => console.error(error));
	}
}

interface IQuote 
{
	hour : number;
	minute : number;
	author : string;
	quote: string;
	timeString: string;
	timeStringInQuote: string;
}