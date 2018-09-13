using System;
using System.Net.Http;

namespace BeerApp.Service.Common
{
	public class BreweryDBClient
	{
		public HttpClient Client { get; }
    
		public BreweryDBClient(HttpClient httpClient)
		{
			Client = httpClient;
		}
	}
}
