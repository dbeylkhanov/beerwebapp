using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace BeerApp.Entities
{
    public class BeerDetail
    {
		public string Id { get; set; }
		
	    public string Name { get; set; }
		
	    public string Description { get; set; }

	    public IEnumerable<Brewery> Breweries { get; set; }

	    public string Ibu { get; set; }

	    public string Abv { get; set; }

	    public string StatusDisplay { get; set; }

	    public BeerStyle Style { get; set; }
    }
}
