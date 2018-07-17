using Newtonsoft.Json;
using System.Net;

namespace BeerApp.Service.Models
{
	public class ResponseModel<T>: IResponseModel
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

		public ResponseModel(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public void SetStatusCode(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}
	}

	public interface IResponseModel
	{
		string ErrorMessage { get; }
		HttpStatusCode StatusCode { get; }
	}
}
