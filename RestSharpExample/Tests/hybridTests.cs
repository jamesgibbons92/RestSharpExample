using NUnit.Framework;
using RestSharp;
using System.Net;
using RestSharpExample.http;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharpExample.gui;

namespace RestSharpExample.Tests
{
	[Parallelizable(ParallelScope.Fixtures)]
	public class hybridTests 
	{
		public static IWebDriver Driver;

		[SetUp]
		public void Startup()
		{
			var chromeOptions = new ChromeOptions();
			// Disable warning "Chrome is being controlled by automated test software" 
			// as this can muck up full page screenshots
			chromeOptions.AddArguments("disable-infobars");

			Driver = new ChromeDriver(chromeOptions);
		}

		[Test]
		[Category("Hybrid")]
		public void StatusCode500()
		{
			//Arrange and act
			//WebDriver opens ChromeDriver and navigates to the page
			string url = "http://the-internet.herokuapp.com/status_codes/500";
			Driver.Navigate().GoToUrl(url);
			pageObjects objects = new pageObjects(Driver);

			//RestSharp sends request
			api API = new api();
			IRestResponse response = API.getStatusCode(url);

			//Assert status code is 500
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

			//Assert Element objects exist
			Assert.That(objects.header.Displayed);
			Assert.That(objects.paragragh.Text.Contains("This page returned a 500 status code"));
		}

		[TearDown]
		public void Cleanup()
		{
			Driver.Quit();
		}
	}
}
