using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Common;
using BeerApp.Service.Interfaces;
using BeerApp.Service.Models;

namespace BeerApp.Service
{
	public class BeerService : IBeerService
	{
		private readonly BreweryDBSettings _settings;

		public BeerService(BreweryDBSettings settings)
		{
			_settings = settings;
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetAllBeers()
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync($"beers?key={_settings.ApiSecretKey}");
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<IEnumerable<BeerStyle>>> GetBeerStyles()
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync($"styles?key={_settings.ApiSecretKey}");
			return await response.ReadAsJsonAsync<IEnumerable<BeerStyle>>();
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetBeersByStyle(int styleId)
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync($"beers?key={_settings.ApiSecretKey}&styleId={styleId}");
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> SearchBeersByQuery(string query)
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync($"search?key={_settings.ApiSecretKey}&q={query}");
			return await response.ReadAsJsonAsync<IEnumerable<Beer>>();
		}

		public async Task<ResponseModel<BeerDetail>> GetBeerById(string id)
		{
			var response = await HttpClientWrapper.GetInstance(_settings.ApiUrl).GetAsync($"beer/{id}?key={_settings.ApiSecretKey}&withBreweries=y");
			return await response.ReadAsJsonAsync<BeerDetail>();
		}
	}
}
