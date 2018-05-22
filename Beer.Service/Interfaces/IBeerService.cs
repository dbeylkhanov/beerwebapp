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
	    /// Get beer styles
	    /// </summary>
	    /// <returns>Response model with collection of beer styles</returns>
	    Task<ResponseModel<IEnumerable<BeerStyle>>> GetBeerStyles();

	    /// <summary>
	    /// Get beer by style
	    /// </summary>
	    /// <returns>Response model with collection of beer styles</returns>
	    Task<ResponseModel<IEnumerable<Beer>>> GetBeersByStyle(int styleId);

	    /// <summary>
	    /// Search beers by query
	    /// </summary>
	    /// <returns>Response model with collection of beers</returns>
	    Task<ResponseModel<IEnumerable<Beer>>> SearchBeersByQuery(string query);

	    /// <summary>
	    /// Get beer by id
	    /// </summary>
	    /// <param name="id">Beer id</param>
	    /// <returns>Response model with Beer POCO</returns>
	    Task<ResponseModel<Beer>> GetBeerById(string id);
    }
}
