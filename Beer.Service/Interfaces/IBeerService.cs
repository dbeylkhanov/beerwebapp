using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Models;

namespace BeerApp.Service.Interfaces
{
    public interface IBeerService
    {
	    /// <summary>
	    /// Get all beers
	    /// </summary>
	    /// <returns>Response model with collection of beers</returns>
	    Task<ResponseModel<IEnumerable<Beer>>> GetAllBeers();

	    /// <summary>
	    /// Get beer by id
	    /// </summary>
	    /// <param name="id">Beer id</param>
	    /// <returns>Response model with Beer POCO</returns>
	    Task<ResponseModel<Beer>> GetBeerById(string id);
    }
}
