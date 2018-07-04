using System.Net.Http;
using System.Threading.Tasks;
using BeerApp.Service.Models;
using Newtonsoft.Json;

namespace BeerApp.Service.Common
{
	public static class HttpResponseExtensions
	{
		public static async Task<ResponseModel<T>> ReadAsJsonAsync<T>(this HttpResponseMessage response)
		{
			using (var content = response.Content)
			{
				string json = await content.ReadAsStringAsync();
				ResponseModel<T> result = JsonConvert.DeserializeObject<ResponseModel<T>>(json);
				result.SetStatusCode(response.StatusCode);

				return result;
			}
		}
	}
}
