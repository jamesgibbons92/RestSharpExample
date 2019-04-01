using RestSharp;

namespace RestSharpExample.http
{
	class api
	{
		public IRestResponse postBasicAuth()
		{
			var client = new RestClient("http://the-internet.herokuapp.com/basic_auth");
			var request = new RestRequest(Method.GET);
			request.AddHeader("cache-control", "no-cache");
			request.AddHeader("Authorization", "Basic YWRtaW46YWRtaW4=");
			request.AddHeader("body", "application/json");
			IRestResponse response = client.Execute(request);
			return response;
		}


		public IRestResponse getStatusCode(string URL)
		{
			var client = new RestClient(URL);
			var request = new RestRequest(Method.GET);
			request.AddHeader("cache-control", "no-cache");
			IRestResponse response = client.Execute(request);
			return response;
		}


	}
}
