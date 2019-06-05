using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace _10WebTask
{
    public class HomePage : CorePage
    {
        [FindsBy(How = How.Id, Using = "fname")]
        private IWebElement FirstName;
        [FindsBy(How = How.Name, Using = "lname")]
        private IWebElement LastName;
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement Email;
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement Password;
        [FindsBy(How = How.Name, Using = "__privateStripeFrame4")]
        private IWebElement CardNumberFrame;
        [FindsBy(How = How.Name, Using = "cardnumber")]
        private IWebElement CardNumber;
        [FindsBy(How = How.Name, Using = "__privateStripeFrame5")]
        private IWebElement ExpDateFrame;
        [FindsBy(How = How.Name, Using = "exp-date")]
        private IWebElement ExpirationDate;
        [FindsBy(How = How.Name, Using = "__privateStripeFrame6")]
        private IWebElement CVCFrame;
        [FindsBy(How = How.Name, Using = "cvc")]
        private IWebElement CVC;
        [FindsBy(How = How.XPath, Using = "//div[@class = 'db-checkout__plan-button']//label[@class = 'db-fc-checkbox sl-sign_up_policy_agree']")]
        private IWebElement CheckBox;
        [FindsBy(How = How.XPath, Using = "//div[@class = 'db-checkout__plan']//a[@class = 'db-button db-button_xl db-button_info db-button_full-width sl-sign_up_create_account_button']")]
        private IWebElement SecureCheckOut;
        [FindsBy(How = How.CssSelector, Using = ".db-server-error")]
        private IWebElement AccountErrorMessage;
        [FindsBy(How = How.Id, Using = "mCSB_1_dragger_vertical")]
        private IWebElement DraggerBar;
        [FindsBy(How = How.Name, Using = "couponCode")]
        private IWebElement CouponCode;
        [FindsBy(How = How.XPath, Using = "//a[@class = 'db-button']")]
        private IWebElement ApplyButton;
        [FindsBy(How = How.XPath, Using = "//div[@class = 'db-panel__body']//p[@class = 'db-checkout__plan-platform']")]
        private IWebElement PayNowText;
        [FindsBy(How = How.XPath, Using = "//div[@class = 'db-checkout__promo-code db-checkout__coupon-code-form coupon_div show']//p[2]")]
        private IWebElement DiscountText;
        [FindsBy(How = How.XPath, Using = "//div[@class = 'db-checkout__promo-code db-checkout__coupon-code-form coupon_div show']//div[2]//h3[1]")]
        private IWebElement DiscountAmount;
        [FindsBy(How = How.XPath, Using = "//div[@class = 'db-checkout__promo-code db-checkout__coupon-code-form coupon_div show']//p[1]")]
        private IWebElement PromoCode;


        public HomePage(IWebDriver driver)
        {
            WebDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void FillAccountInfo(string firstName, string lastName, string password)
        {
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            string email = "Test" + randomInt.ToString() + "@mail.ru";
            FirstName.Clear();
            FirstName.SendKeys(firstName);
            LastName.Clear();
            LastName.SendKeys(lastName);
            Email.Clear();
            Email.SendKeys(email);
            Password.Clear();
            Password.SendKeys(password);
        }

        public void FillValidCardNumber()
        {
            string cardNumber = ConfigurationManager.AppSettings["Valid test card"];
            WebDriver.SwitchTo().Frame(CardNumberFrame);
            CardNumber.Clear();
            CardNumber.SendKeys(cardNumber);
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
        }

        public void FillInvalidCardNumber()
        {
            string cardNumber = ConfigurationManager.AppSettings["Invalid test card"];
            WebDriver.SwitchTo().Frame(CardNumberFrame);
            CardNumber.Clear();
            CardNumber.SendKeys(cardNumber);
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
        }

        public void FillExpirationDate(string date)
        {
            WebDriver.SwitchTo().Frame(ExpDateFrame);
            ExpirationDate.Clear();
            ExpirationDate.SendKeys(date);
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
        }

        public void FillCvc(string cvc)
        {
            WebDriver.SwitchTo().Frame(CVCFrame);
            CVC.Clear();
            CVC.SendKeys(cvc);
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles[0]);
        }

        public void CheckCheckBox()
        {
            try
            {
                CheckBox.Click();
            }
            catch (Exception)
            {
                IJavaScriptExecutor executer = (IJavaScriptExecutor)WebDriver;
                executer.ExecuteScript("arguments[0].setAttribute('style', 'top:0px')", DraggerBar);
                CheckBox.Click();
            }
        }

        public void ClickOnCheckOutButton()
        {
            SecureCheckOut.Click();
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("(//div[@class = 'db-loading-spinner__double-bounce'])[1]")));
        }

        public bool InvalidCardNumberVerification(string message)
        {
            return AccountErrorMessage.Displayed && AccountErrorMessage.Text == message;
        }

        public void FillInvalidCouponCode()
        {
            string invalidCouponCode = ConfigurationManager.AppSettings["Invalid coupon"];
            CouponCode.Clear();
            CouponCode.SendKeys(invalidCouponCode);
        }

        public void ClickOnApplyButton()
        {
            ApplyButton.Click();
        }

        public bool InvalidCouponVerification(string message)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
            return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//a[@class = 'db-button']/../..//div[@class = 'db-server-error-wrap']"))).First().Text == message;
        }

        public bool ValidCouponVerification()
        {
            string validCoupon = ConfigurationManager.AppSettings["Valid coupon"];
            CouponCode.Clear();
            CouponCode.SendKeys(validCoupon);
            ApplyButton.Click();
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class = 'db-checkout__promo-code db-checkout__coupon-code-form coupon_div show']")));
            double initialCost = Convert.ToDouble(PayNowText.Text.Split('$')[1].Replace(" ", string.Empty), CultureInfo.InvariantCulture);
            double costAfterDiscount = Convert.ToDouble(PayNowText.Text.Split('$').LastOrDefault().Split(' ').First(), CultureInfo.InvariantCulture);
            double discount = Convert.ToDouble(DiscountText.Text.Split(' ')[1].Split('%').First(), CultureInfo.InvariantCulture);
            double discountAmount = Convert.ToDouble(DiscountAmount.Text.Split('$').Last(), CultureInfo.InvariantCulture);
            bool state = PayNowText.Displayed && PromoCode.Displayed && DiscountText.Displayed && DiscountAmount.Displayed &&
                ((initialCost * discount) / 100 == costAfterDiscount) && costAfterDiscount == discountAmount;
            return state;
        }
    }
}
