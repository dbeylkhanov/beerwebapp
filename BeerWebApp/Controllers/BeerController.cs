using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BeerApp.Bll.Beers;
using Microsoft.AspNetCore.Mvc;

namespace BeerApp.Web.Controllers
{
	[Route("api")]
	public class BeerController : Controller
	{
		private readonly IBeerManager _beerManager;

		public BeerController(IBeerManager beerManager)
		{
			_beerManager = beerManager;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Beers(string query)
		{
			var result = await _beerManager.GetBeers(query);

			return Ok(result);
		}

		[HttpGet]
		[Route("[action]/{id}")]
		public async Task<IActionResult> Beer([Required]string id)
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
