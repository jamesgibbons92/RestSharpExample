using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using RestSharpExample.DataEntities;
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



		[Test]
		public void DogStatusContentType()
		{
			// arrange
			RestClient client = new RestClient("https://dog.ceo/api/");
			RestRequest request = new RestRequest("breeds/list/all ", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			// assert
			Assert.That(response.ContentType, Is.EqualTo("application/json"));
		}


		[Test]
		public void DogResponseStatusCode200()
		{
			// arrange
			RestClient client = new RestClient("https://dog.ceo/api/");
			RestRequest request = new RestRequest("breeds/list/all", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			// assert
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}


		[Test]
		public void DogExpectedContent()
		{
			// arrange
			RestClient client = new RestClient("https://dog.ceo/api/");
			RestRequest request = new RestRequest("breed/bulldog/french/images", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			
			// assert
			Assert.That(response.Content.Contains("https://images.dog.ceo/breeds/bulldog-french/n02108915_10204.jpg"));
		}
	}
}
