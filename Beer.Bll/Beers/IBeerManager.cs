using System.Collections.Generic;
using System.Threading.Tasks;
using BeerApp.Entities;
using BeerApp.Service.Models;

namespace BeerApp.Bll.Beers
{
    public interface IBeerManager
    {
	    Task<ResponseModel<IEnumerable<Beer>>> GetBeers(string query = default (string), int? styleId = null);

	    Task<ResponseModel<IEnumerable<BeerStyle>>> GetBeerStyles();

	    Task<ResponseModel<BeerDetail>> GetBeerById(string id);

	    void InitBreweryDbConnectionString(string url, string token);
    }
}

