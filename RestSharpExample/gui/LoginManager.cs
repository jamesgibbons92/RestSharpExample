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
			}
	}
}
