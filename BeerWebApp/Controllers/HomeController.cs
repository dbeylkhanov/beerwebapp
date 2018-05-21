using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BeerApp.Bll.Beers;
using Microsoft.AspNetCore.Mvc;

namespace BeerApp.Web.Controllers
{
	[Route("api/Home")]
    public class HomeController : Controller
    {
	    private readonly IBeerManager _beerManager;

	    public HomeController(IBeerManager beerManager)
	    {
		    _beerManager = beerManager;
	    }

		[HttpGet]
		[Route("Beers")]
        public async Task<IActionResult> GetAllBeers()
		{
			var result = await _beerManager.GetAllBeers();

			return Ok(result);
		}

	    [HttpGet]
	    [Route("Beer/{id}")]
	    public async Task<IActionResult> GetBeerById([Required]string id)
	    {
		    if (ModelState.IsValid)
		    {
			    var result = await _beerManager.GetBeerById(id);

			    return Ok(result);
		    }

		    return BadRequest("Beer Id is required");
	    }
    }
}
