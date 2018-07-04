namespace BeerApp.Entities
{
    public class BreweryDBSettings
    { 
	    public string ApiUrl { get; set; }
	    public string ApiSecretKey{ get; set; }

	    public void Init(string url, string secretKey)
	    {
		    ApiUrl = url;
		    ApiSecretKey = secretKey;
	    }
    }
}
