using NUnit.Framework;
using OpenQA;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace Callhub
{
    public class Tests
    {
        //Automation Testing of CallHub Pricing Page -  Validate the pricing of various subscriptions (Monthly/Quarterly/Half-yearly/Yearly) for all the plans on the page.

        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://callhub.io/pricing/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        [Test]
        public void Test_ValidatePricing()
        {
            Pricing pricingPage = new Pricing(driver);
            List<string> priceTypes = new List<string> { "Monthly", "Quarterly", "Half-yearly", "Yearly"};
            //Validate prices
            pricingPage.ValidatePricing(priceTypes);


        }


        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }
    }
}