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
            string sessionCookieName = "rack.session";
            var cookieValue = Driver.Manage().Cookies.GetCookieNamed(sessionCookieName).Value.ToString();

            api API = new api(Driver);
            IRestResponse response = API.postLogin(sessionCookieName, cookieValue);
            //IRestResponse SecurePage = API.secure(cookieName, cookieValue);
            //Driver.Navigate().GoToUrl(response.ResponseUri);

            //Assert 
            Assert.That(objects.secretAreaHeader.Text.Contains("Secure Area"));
        }



        [TearDown]
        public void Cleanup()
        {
            Driver.Quit();
        }
    }
}
