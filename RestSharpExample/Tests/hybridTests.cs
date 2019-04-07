using NUnit.Framework;
using RestSharp;
using System.Net;
using RestSharpExample.http;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharpExample.gui;
using Cookie = OpenQA.Selenium.Cookie;
using System.Collections.Generic;

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
		[Category("Build")]
		public void StatusCode500()
		{
			//Arrange and act
			//WebDriver opens ChromeDriver and navigates to the page
			string url = "http://the-internet.herokuapp.com/status_codes/500";
			Driver.Navigate().GoToUrl(url);
			pageObjects objects = new pageObjects(Driver);

			//RestSharp sends request
			api API = new api(Driver);
			IRestResponse response = API.getStatusCode(url);

			//Assert status code is 500
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

			//Assert Element objects exist
			Assert.That(objects.header.Displayed);
			Assert.That(objects.paragragh.Text.Contains("This page returned a 500 status code"));
		}


		[Test]
		[Category("Build")]
		public void BasicAuth()
		{
			//Arrange 
			//Using basic auth in selenium 
			Driver.Navigate().GoToUrl("http://admin:admin@the-internet.herokuapp.com/basic_auth");
			pageObjects objects = new pageObjects(Driver);

			//Assert 
			Assert.That(objects.congratulations.Text.Contains("congratulations"));

		}

		[Test]
		[Category("Build")]
		public void postLogin()
		{
			//Arrange 
			pageObjects objects = new pageObjects(Driver);
			Driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/login");

			var text = Driver.Manage().Cookies.GetCookieNamed("rack.session").ToString();

			string[] sepChars = {"="};
			string[] newValue = text.Split(sepChars, System.StringSplitOptions.RemoveEmptyEntries);
			string cookieName = string.Empty;
			string cookieValue = string.Empty;
			#region trim of cookies
			List<string> actual = new List<string>();
			foreach (var item in newValue)
			{
				actual.Add(item);				
			}

			foreach (var item2 in actual)
			{
				if (actual[1].Contains("BAh7"))
				{
					actual.ToString();
					char[] MyChar = { 'p', 'a', 't', 'h', ' ', ';' };
					cookieValue = actual[1].ToString().TrimEnd(MyChar);
				}

				if (actual[0].Contains("rack.session"))
				{
					actual.ToString();
					cookieName = actual[0].ToString();
				}
				break;
			}
			#endregion
			api API = new api(Driver);
			IRestResponse response = API.postLogin(cookieName, cookieValue);
			//IRestResponse SecurePage = API.secure(cookieName, cookieValue);
			//Driver.Navigate().GoToUrl(response.ResponseUri);

			//Assert 
			Assert.That(objects.secretAreaHeader.Text.Contains("You logged into a secure area!"));
		}

		

		[TearDown]
		public void Cleanup()
		{
			Driver.Quit();
		}
	}
}
