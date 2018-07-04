using System.Net;
using Newtonsoft.Json;

namespace BeerApp.Service.Models
{
	public class ResponseModel<T>
	{
		public string ErrorMessage { get; private set; }
		public HttpStatusCode StatusCode { get; private set; }
		public T Data { get; private set; }

		[JsonConstructor]
		public ResponseModel(T data, string errorMessage = null)
		{
			Data = data;
			ErrorMessage = errorMessage;
		}


		public void SetStatusCode(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}
	}
}
