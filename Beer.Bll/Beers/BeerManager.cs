using System.Collections.Generic;
using System.Linq;
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

		public async Task<IEnumerable<Beer>> GetBeers(string query = default(string), int? styleId = null)
		{
			if (!string.IsNullOrWhiteSpace(query))
			{
				var searchResult = await _beerService.SearchBeersByQuery(query);
				return searchResult.Data;
			}

			if (styleId.HasValue && styleId.Value > 0)
			{
				var beersByStyleResult = await _beerService.GetBeersByStyle(styleId.Value);
				return beersByStyleResult.Data;
			}

			var result = await _beerService.GetAllBeers();
			return result.Data;
		}

		public async Task<IEnumerable<BeerStyle>> GetBeerStyles()
		{
			var result = await _beerService.GetBeerStyles();
			return result.Data.OrderBy(x => x.Name);
		}

		public async Task<BeerDetail> GetBeerById(string id)
		{
			var result = await _beerService.GetBeerById(id);
			return result.Data;
		}
	}
}
