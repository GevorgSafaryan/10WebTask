using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10WebTask
{
    public class Tests : BaseTest
    {
        [TestCase("Test", "Test", "Testtest", "1220", "777", "Your card has insufficient funds.", TestName = "INVALID CHECKOUT CASE")]
        public void InvalidChekOut(string firstName, string lastName, string password, string expDate, string cvc, string message)
        {
            HomePage.FillAccountInfo(firstName, lastName, password);
            HomePage.FillInvalidCardNumber();
            HomePage.FillExpirationDate(expDate);
            HomePage.FillCvc(cvc);
            HomePage.CheckCheckBox();
            HomePage.ClickOnCheckOutButton();
            Assert.IsTrue(HomePage.InvalidCardNumberVerification(message));
        }

        [TestCase("This coupon code is invalid or has expired.", TestName = "INVALID COUPON APPLY CASE")]
        public void InvalidCouponApply(string message)
        {
            HomePage.FillInvalidCouponCode();
            HomePage.ClickOnApplyButton();
            Assert.IsTrue(HomePage.InvalidCouponVerification(message));
        }

        [Test]
        public void ValidCouponApply()
        {
            Assert.IsTrue(HomePage.ValidCouponVerification());
        }

        [TestCase("Test", "Test", "Testtest", "1220", "777", TestName = "VALID CHECKOUT CASE")]
        public void ValidChekOut(string firstName, string lastName, string password, string expDate, string cvc)
        {
            HomePage.FillAccountInfo(firstName, lastName, password);
            HomePage.FillValidCardNumber();
            HomePage.FillExpirationDate(expDate);
            HomePage.FillCvc(cvc);
            HomePage.CheckCheckBox();
            HomePage.ClickOnCheckOutButton();
            Assert.IsTrue(WebSitesPage.ValidCardNumberVerification());
        }
    }
}
