using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Models;

namespace BeerApp.Bll.Beers
{
    public interface IBeerManager
    {
	    Task<IEnumerable<Beer>> GetAllBeers();

	    Task<Beer> GetBeerById(string id);
    }
}
