using NUnit.Framework;
using RestSharp;
using System.Net;

namespace RestSharpExample.Tests
{
	[TestFixture]
	public class APITests
	{
		[Test]
		public void StatusCodeOK()
		{
			// arrange
			RestClient client = new RestClient("http://api.zippopotam.us");
			RestRequest request = new RestRequest("nl/3825", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			// assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}


		[Test]
		public void ContentTypeTest()
		{
			// arrange
			RestClient client = new RestClient("http://api.zippopotam.us");
			RestRequest request = new RestRequest("nl/3825", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			// assert
			Assert.That(response.ContentType, Is.EqualTo("application/json"));
		}


		[TestCase("nl", "3825", HttpStatusCode.OK, TestName = "Check status code for NL zip code 7411")]
		[TestCase("lv", "1050", HttpStatusCode.NotFound, TestName = "Check status code for LV zip code 1050")]
		public void StatusCodeTest(string countryCode, string zipCode, HttpStatusCode expectedHttpStatusCode)
		{
			// arrange
			RestClient client = new RestClient("http://api.zippopotam.us");
			RestRequest request = new RestRequest($"{countryCode}/{zipCode}", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			// assert
			Assert.That(response.StatusCode, Is.EqualTo(expectedHttpStatusCode));
		}

	}
}
