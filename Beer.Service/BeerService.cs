using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Common;
using BeerApp.Service.Interfaces;
using BeerApp.Service.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace BeerApp.Service
{
	public class BeerService : IBeerService
	{
		private readonly BreweryDBSettings _settings;
		private readonly Dictionary<string, string> queryStringParams;

		public BeerService(BreweryDBSettings settings)
		{
			_settings = settings;
			queryStringParams = new Dictionary<string, string>() { { "key", _settings.ApiSecretKey } };
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetAllBeers()
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync(QueryHelpers.AddQueryString("beers", queryStringParams));
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<IEnumerable<BeerStyle>>> GetBeerStyles()
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync(QueryHelpers.AddQueryString("styles", queryStringParams));
			return await response.ReadAsJsonAsync<IEnumerable<BeerStyle>>();
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetBeersByStyle(int styleId)
		{
			queryStringParams.Add("styleId", $"{styleId}");
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync(QueryHelpers.AddQueryString("beers", queryStringParams));
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> SearchBeersByQuery(string query)
		{
			queryStringParams.Add("q", $"{query}");
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync(QueryHelpers.AddQueryString("search", queryStringParams));
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<BeerDetail>> GetBeerById(string id)
		{
			queryStringParams.Add("withBreweries", "y");
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync(QueryHelpers.AddQueryString($"beer/{id}", queryStringParams));
			return await response.ReadAsJsonAsync<BeerDetail>();
		}
	}
}
