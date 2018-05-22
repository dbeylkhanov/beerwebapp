using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Models;

namespace BeerApp.Bll.Beers
{
    public interface IBeerManager
    {
	    Task<IEnumerable<Beer>> GetBeers(string query = default (string), int? styleId = null);

	    Task<IEnumerable<BeerStyle>> GetBeerStyles();

	    Task<Beer> GetBeerById(string id);
    }
}

