using System;
using System.Net;
using System.Net.Http;

namespace BeerApp.Service.Common
{
	// Use for unit tests
	public sealed class HttpClientWrapper
	{
		private static HttpClient _client;

		public static HttpClient GetInstance(string baseAddress)
		{
			if (_client == null)
			{
				var handler = new HttpClientHandler();
				if (handler.SupportsAutomaticDecompression)
				{
					handler.AutomaticDecompression = DecompressionMethods.GZip |
					                                 DecompressionMethods.Deflate;
				}
				_client = new HttpClient(handler)
				{
					BaseAddress = new Uri(baseAddress)
				};
			}

			return _client;
		}
	}

}
