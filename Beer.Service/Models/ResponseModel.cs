namespace BeerApp.Service.Models
{
    public class ResponseModel<T>
    {
	    public string ErrorMessage { get; set; }

	    public T Data { get; set; }
    }
}
