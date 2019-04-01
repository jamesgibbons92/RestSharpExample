using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


		public IRestResponse get500()
		{
			var client = new RestClient("http://the-internet.herokuapp.com/status_codes/500");
			var request = new RestRequest(Method.GET);
			request.AddHeader("cache-control", "no-cache");
			IRestResponse response = client.Execute(request);
			return response;
		}


	}
}
