using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpExample.gui
{
	public class LoginManager
	{
		

			private IWebDriver driver;

			public LoginManager(IWebDriver webDriver)
			{
				driver = webDriver;
			}

			public void setLoginCookies(string name, string value)
			{
				Cookie cookie = new Cookie(name, value);
				driver.Manage().Cookies.AddCookie(cookie);
			    Cookie cookie2 = new Cookie("optimizelyEndUserId", "oeu1554041438837r0.6506281397878788414");
			    driver.Manage().Cookies.AddCookie(cookie2);
			}
	}
}
