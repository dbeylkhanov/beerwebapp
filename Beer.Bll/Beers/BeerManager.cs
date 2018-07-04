using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Service.Interfaces;
using BeerApp.Service.Models;
using BeerApp.Entities;

namespace BeerApp.Bll.Beers
{
	public class BeerManager : IBeerManager
	{
		private readonly IBeerService _beerService;

		public BeerManager(IBeerService beerService)
		{
			_beerService = beerService;
		}

		public async Task<ResponseModel<IEnumerable<Beer>>> GetBeers(string query = default(string), int? styleId = null)
		{
			if (!string.IsNullOrWhiteSpace(query))
			{
				var searchResult = await _beerService.SearchBeersByQuery(query);
				return searchResult;
			}

			if (styleId.HasValue && styleId.Value > 0)
			{
				var beersByStyleResult = await _beerService.GetBeersByStyle(styleId.Value);
				return beersByStyleResult;
			}

			var result = await _beerService.GetAllBeers();
			return result;
		}

		public async Task<ResponseModel<IEnumerable<BeerStyle>>> GetBeerStyles()
		{
			var result = await _beerService.GetBeerStyles();
			return result;
		}

		public async Task<ResponseModel<BeerDetail>> GetBeerById(string id)
		{
			var result = await _beerService.GetBeerById(id);
			return result;
		}

		public void InitBreweryDbConnectionString(string url, string token)
		{
			_beerService.InitBreweryDbConnectionString(url, token);
		}
	}
}
