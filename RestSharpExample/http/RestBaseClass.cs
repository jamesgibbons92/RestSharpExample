//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Text.RegularExpressions;
//using log4net;
//using RestSharp;
//using System.Configuration;
//using RestSharp.Deserializers;
//using NUnit.Framework;
//using System.Net;

//namespace RestSharpExample.http
//{
//	class RestBaseClass
//	{

//		private readonly string _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

//		public static RestClient Client;
//		//public TestContext TestContext { get; set; }
//		public static string StaticToken;
//		// For additional logging with Log4Net
//		public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//		protected RestBaseClass()
//		{
//		}


//		[SetUp]
//		public void InitialRequest()
//		{
//			// Initialise RestClient
//			Client = new RestClient(_baseUrl);

//			// Initial homepage request
//			var requestHomePage = new RestRequest("/", Method.GET);
//			var responseHomePage = Client.Execute(requestHomePage);

//			// Set cookies
//			SetCookies(responseHomePage);
//			// Get static token
//			GetStaticToken(responseHomePage);

//		}


//		private static void SetCookies(IRestResponse response)
//		{
//			var cookieJar = new CookieContainer();

//			// If responds with 200 then set cookie
//			if (response.StatusCode == HttpStatusCode.OK)
//			{
//				var cookie = response.Cookies.FirstOrDefault();
//				if (cookie != null) cookieJar.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
//			}
//			Client.CookieContainer = cookieJar;
//		}

//		private static void GetStaticToken(IRestResponse response)
//		{
//			// Get token
//			var match = Regex.Match(response.Content, @"static_token \= \'([A-Za-z0-9\-]+)\'\;", RegexOptions.IgnoreCase);
//			StaticToken = match.Groups[1].Value;
//			Console.WriteLine(StaticToken);
//		}

//		//public object JsonDeserializer(IRestResponse response)
//		//{
//		//	//var deserializer = new JsonDeserializer();
//		//	//Log.Info(response.Content);
//		//	//var obj = deserializer.Deserialize<deserializer>(response);
//		//	//return obj;
//		//}
//	}
//}
