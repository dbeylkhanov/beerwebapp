using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Common;
using BeerApp.Service.Interfaces;
using BeerApp.Service.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BeerApp.Service
{
	public class BeerService : IBeerService
	{
		private readonly IOptions<BreweryDBSettings> _settings;

		public BeerService(IOptions<BreweryDBSettings> settings)
		{
			_settings = settings;
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetAllBeers()
		{
			var response = await HttpClientWrapper.GetInstance(_settings.Value.ApiUrl).GetAsync($"beers?key={_settings.Value.ApiSecretKey}");
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<IEnumerable<BeerStyle>>> GetBeerStyles()
		{
			var response = await HttpClientWrapper.GetInstance(_settings.Value.ApiUrl).GetAsync($"styles?key={_settings.Value.ApiSecretKey}");
			return await response.ReadAsJsonAsync<IEnumerable<BeerStyle>>();
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetBeersByStyle(int styleId)
		{
			var response = await HttpClientWrapper.GetInstance(_settings.Value.ApiUrl).GetAsync($"beers?key={_settings.Value.ApiSecretKey}&styleId={styleId}");
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> SearchBeersByQuery(string query)
		{
			var response = await HttpClientWrapper.GetInstance(_settings.Value.ApiUrl).GetAsync($"search?key={_settings.Value.ApiSecretKey}&q={query}");
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<BeerDetail>> GetBeerById(string id)
		{
			var response = await HttpClientWrapper.GetInstance(_settings.Value.ApiUrl).GetAsync($"beer/{id}?key={_settings.Value.ApiSecretKey}&withBreweries=y");
			return await response.ReadAsJsonAsync<BeerDetail>();
		}

		public void InitBreweryDbConnectionString(string url, string token)
		{
			_settings.Value.Init(url, token);
		}
	}
}
