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

		public async Task<IEnumerable<Beer>> GetAllBeers()
		{
			var result = await _beerService.GetAllBeers();

			return result.Data;
		}

		public async Task<Beer> GetBeerById(string id)
		{
			var result = await _beerService.GetBeerById(id);

			return result.Data;
		}
	}
}
