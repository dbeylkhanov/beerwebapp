using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BeerApp.Bll.Beers;
using Microsoft.AspNetCore.Mvc;

namespace BeerApp.Web.Controllers
{
    public class HomeController : Controller
    {
	   
	    
	    public IActionResult Index()
	    {
		    return View();
	    }

	
    }
}
