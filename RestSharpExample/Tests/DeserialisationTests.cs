using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using RestSharpExample.DataEntities;

namespace RestSharpExamples.Tests
{
	[TestFixture]
	public class DeserializationTests
	{
		/// <summary>
		/// Test using example from https://www.ontestautomation.com/restful-api-testing-in-csharp-with-restsharp/
		/// </summary>
		[Test]
		[Category("Build")]
		public void CountryAbbreviationSerializationTest()
		{
			// arrange
			RestClient client = new RestClient("http://api.zippopotam.us");
			RestRequest request = new RestRequest("us/90210", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			LocationResponse locationResponse =
				new JsonDeserializer().
				Deserialize<LocationResponse>(response);

			// assert
			Assert.That(locationResponse.CountryAbbreviation, Is.EqualTo("US"));
		}


		/// <summary>
		/// Test using example from https://www.ontestautomation.com/restful-api-testing-in-csharp-with-restsharp/
		/// </summary>
		[Test]
		[Category("Build")]
		public void StateSerializationTest()
		{
			// arrange
			RestClient client = new RestClient("http://api.zippopotam.us");
			RestRequest request = new RestRequest("us/12345", Method.GET);

			// act
			IRestResponse response = client.Execute(request);

			LocationResponse locationResponse =
				new JsonDeserializer().
				Deserialize<LocationResponse>(response);

			// assert
			Assert.That(locationResponse.Places[0].State, Is.EqualTo("New York"));
		}
	}
}