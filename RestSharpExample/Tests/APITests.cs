using NUnit.Framework;
using RestSharp;
using System.Net;

namespace RestSharpExample.Tests
{
	[TestFixture]
	public class APITests
	{
		/// <summary>
		/// Test using example from https://www.ontestautomation.com/restful-api-testing-in-csharp-with-restsharp/
		/// </summary>
		[Test]
		[Category("Build")]
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


		/// <summary>
		/// Test using example from https://www.ontestautomation.com/restful-api-testing-in-csharp-with-restsharp/
		/// </summary>
		[TestCase("nl", "7411", HttpStatusCode.OK, TestName = "Check status code for NL zip code 7411")]
		[TestCase("lv", "1050", HttpStatusCode.NotFound, TestName = "Check status code for LV zip code 1050")]
		[Category("Build")]
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


		/// <summary>
		/// Test using API from https://dog.ceo/dog-api/documentation/
		/// </summary>
		[Test]
		[Category("Build")]
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


		/// <summary>
		/// Test using API from https://dog.ceo/dog-api/documentation/
		/// </summary>
		[Test]
		[Category("Build")]
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


		/// <summary>
		/// Test using API from https://dog.ceo/dog-api/documentation/
		/// </summary>
		[Test]
		[Category("Build")]
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


		/// <summary>
		/// Test using API from https://dog.ceo/dog-api/documentation/
		/// </summary>
		[TestCase("bulldog", "french", HttpStatusCode.OK, TestName = "Check status code for french bulldog")]
		[TestCase("bulldog", "boston", HttpStatusCode.OK, TestName = "Check status code for boston bulldog")]
		[TestCase("bulldog", "rob", HttpStatusCode.NotFound, TestName = "Check status code for rob bulldog")]
		[Category("Build")]
		public void DogResponseStatusCodeTestCase(string breed, string subBreed, HttpStatusCode expectedHttpStatusCode)
		{
			// arrange
			RestClient client = new RestClient("https://dog.ceo/api/");
			RestRequest request = new RestRequest($"/breed/{breed}/{subBreed}/images", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			// assert
			Assert.That(response.StatusCode, Is.EqualTo(expectedHttpStatusCode));
		}

	}
}
