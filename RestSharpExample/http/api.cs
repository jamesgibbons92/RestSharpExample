using RestSharp;
using System.Collections.Generic;
using RestSharpExample.gui;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using System.Net;
using Cookie = OpenQA.Selenium.Cookie;

namespace RestSharpExample.http
{
    public class api
    {

        private IWebDriver driver;

        public api(IWebDriver webDriver)
        {
            driver = webDriver;
        }

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

        /// <summary>
        /// PostLogin for /authenticate POST
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IRestResponse postLogin(string name, string value)
        {

            //app session cookie stores the user-agent and accept language in the session cookie so these need to match in the selenium and restsharp requests
            //so we set the user agent and accept language headers below. 
            var client = new RestClient("http://the-internet.herokuapp.com/authenticate");
            client.CookieContainer = new CookieContainer();
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Safari/537.36";
            var request = new RestRequest(Method.POST);


            AddrequestHeaders(request);

            //Add first page load cookies from /login
            //request.AddCookie(name, value);
            IRestResponse response = client.Execute(request);
            //Set the current response cookie name and value after a successful OK
            LoginManager loginCookie = new LoginManager(driver);
            //driver.Manage().Cookies.AddCookie(response.Cookies[0]);
            //string cookieName = extractCookieName(response);
            //string cookieValue = extractCookieValue(response);

            var convertedCookie = convertRestSharpToSeleniumCookie(response.Cookies.SingleOrDefault(x => x.Name == name));
            //loginCookie.setLoginCookies(convertedCookie);
            driver.Manage().Cookies.AddCookie(convertedCookie);
            //driver.Navigate().Refresh();
            driver.Navigate().GoToUrl(response.ResponseUri);

            return response;
        }

        private static Cookie convertRestSharpToSeleniumCookie(RestResponseCookie c)
        {
            //Cookie.FromDictionary allows us to set all cookie properties using a dictionary.
            //We create the raw cookie dictionary using the properties from the RestResponseCookie

            Dictionary<string, object> rawCookie = new Dictionary<string, object> { };

            if (c.Name != null) { rawCookie["name"] = c.Name; }
            if (c.Domain != null) { rawCookie["domain"] = c.Domain; };
            if (c.Value != null) { rawCookie["value"] = c.Value; };
            if (c.Path != null) { rawCookie["path"] = c.Path; };
            rawCookie["httpOnly"] = c.HttpOnly ? "true" : "false";
            rawCookie["secure"] = c.Secure ? "true" : "false";
            if (c.Expires != null) { rawCookie["expiry"] = c.Expires; };

            Cookie cookie = Cookie.FromDictionary(rawCookie);


            return cookie;
        }

        /// <summary>
        /// Get the response cookie Name
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string extractCookieName(IRestResponse response)
        {
            return response.Cookies[0].Name.ToString();
        }

        /// <summary>
        /// Get the response cookie value
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string extractCookieValue(IRestResponse response)
        {
            return response.Cookies[0].Value.ToString();
        }

        /// <summary>
        /// Request Headers for /authenticate request POST
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IRestRequest AddrequestHeaders(RestRequest request)
        {

            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Host", "the-internet.herokuapp.com");
            //request.AddHeader("Connection", "keep-alive");
            //request.AddHeader("Content-Length", "49");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("Origin", "http://the-internet.herokuapp.com");
            //request.AddHeader("Upgrade-Insecure-Requests", "1");
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Safari/537.36");
            //request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            //request.AddHeader("Referer", "http://the-internet.herokuapp.com/login");
            //request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "en-US,en;q=0.9");
            //request.AddCookie("optimizelySegments", "%7B%7D");
            //request.AddCookie("optimizelyBuckets", "%7B%7D");
            //request.AddCookie("optimizelyEndUserId", "oeu1554041438837r0.6506281397878788414");
            request.AddParameter("application/x-www-form-urlencoded", "username=tomsmith&password=SuperSecretPassword!", ParameterType.RequestBody);
            return request;
        }




        //public IRestResponse secure(string name, string value)
        //{
        //	var client = new RestClient("http://the-internet.herokuapp.com/secure");
        //	var request = new RestRequest(Method.GET);
        //	request.AddHeader("Cookie", name+"="+value);
        //	//request.AddHeader("Cookie", "rack.session=BAh7CEkiDXVzZXJuYW1lBjoGRUZJIg10b21zbWl0aAY7AFRJIgpmbGFzaAY7%0AAEZ7BjoMc3VjY2Vzc0kiI1lvdSBsb2dnZWQgaW50byBhIHNlY3VyZSBhcmVh%0AIQY7AFRJIg9zZXNzaW9uX2lkBjsAVEkiRTE5ZDVjNzY3MzIwMDY4ZTc3MDIx%0AOThlZmQ2MTRkOGM0YWE4ZTNkMDlhOTY1ZTBhMzZmZmJhZWUxYTU3ZjJmYjMG%0AOwBG%0A--2158ac2b278f76f51144e4444f3f2827d6dd6187; rack.session=BAh7CUkiD3Nlc3Npb25faWQGOgZFVEkiRTE5ZDVjNzY3MzIwMDY4ZTc3MDIx%0AOThlZmQ2MTRkOGM0YWE4ZTNkMDlhOTY1ZTBhMzZmZmJhZWUxYTU3ZjJmYjMG%0AOwBGSSIJY3NyZgY7AEZJIiUwM2JlNDkyNjA0YzhiZDQ4MzA5OTFiNTE5ZmI2%0AY2Q4YgY7AEZJIg10cmFja2luZwY7AEZ7B0kiFEhUVFBfVVNFUl9BR0VOVAY7%0AAFRJIi02MWJjYjA2ZmQ0YTJiMTBjMTQzMjk3ZmFiOGIzMzRlODU3NWM2NDZl%0ABjsARkkiGUhUVFBfQUNDRVBUX0xBTkdVQUdFBjsAVEkiLTkzZTkwN2E1MWIy%0ANjRkZjBlNjE3MThkMDcyZWZhNzUxMjI2ZDdkYWYGOwBGSSIKZmxhc2gGOwBG%0AewA%3D%0A--5f7f129c97d384e2bf4fd88a4d3d0b2ff454d76d");
        //	request.AddHeader("Host", "the-internet.herokuapp.com");
        //	request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //	request.AddHeader("Origin", "http://the-internet.herokuapp.com");
        //	request.AddHeader("Upgrade-Insecure-Requests", "1");
        //	request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
        //	request.AddHeader("Referer", "http://the-internet.herokuapp.com/login");
        //	request.AddHeader("Accept-Encoding", "gzip, deflate");
        //	request.AddHeader("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");
        //	request.AddHeader("cache-control", "no-cache,max-age=0,no-cache");
        //	request.AddHeader("GET /secure HTTP/1.1", "");
        //	IRestResponse response = client.Execute(request);
        //	return response;
        //}

    }
}
