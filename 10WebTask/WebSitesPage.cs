using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10WebTask
{
    public class WebSitesPage : HomePage
    {
        [FindsBy(How = How.XPath, Using = "//a[@class = 'sl-add-website-button db-button db-button_info db-button_lg ng-star-inserted']")]
        private IWebElement AddWebsiteButton;

        public WebSitesPage(IWebDriver driver) : base(driver)
        {

        }



        public bool ValidCardNumberVerification()
        {
            return WebDriver.Title == "Websites - Dashboard" && AddWebsiteButton.Displayed;
        }
    }
}
