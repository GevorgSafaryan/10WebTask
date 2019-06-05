using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10WebTask
{
    public class CorePage
    {
        public string URL { get; set; }
        public string Browser { get; set; }
        public IWebDriver WebDriver { get; set; }

        public void Init()
        {
            URL = ConfigurationManager.AppSettings["url"];
            Browser = ConfigurationManager.AppSettings["browser"];
            switch (Browser)
            {
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--start-maximized", "disable-infobars");
                    WebDriver = new ChromeDriver(options);
                    WebDriver.Url = URL;
                    break;
                case "firefox":
                    break;
                case "explorer":
                    break;
                case "opera":
                    break;
                case "safari":
                    break;
                default:
                    throw new Exception("Usported browser");
            }
        }

        public void CleanUp()
        {
            WebDriver.Dispose();
        }
    }
}
