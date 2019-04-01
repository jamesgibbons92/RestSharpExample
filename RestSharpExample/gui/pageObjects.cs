using OpenQA.Selenium;

namespace RestSharpExample.gui
{
	public class pageObjects
	{
		private IWebDriver _driver;
		public pageObjects(IWebDriver Driver)
		{
			_driver = Driver;
		}

		public IWebElement paragragh => _driver.FindElement(By.CssSelector("p"));

		public IWebElement header => _driver.FindElement(By.CssSelector("h3"));
	}
}
