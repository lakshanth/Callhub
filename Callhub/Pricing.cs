using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Callhub
{
    public class Pricing
    {
        IWebDriver driver;
        public Pricing(IWebDriver webdriver)
        {
            this.driver = webdriver;
        }

        public void ClickOnPricingCircle(string mode)
        {
            Thread.Sleep(2000);
            IWebElement pricingCircle = driver.FindElement(By.XPath("//*[@class='plan-pricing-timeline']//*[text()='" + mode + "']"));
            pricingCircle.Click();
        }

        public void ValidatePriceOnCard(List<string> priceList)
        {
            foreach(string price in priceList)
            {
                try
                {
                    //Assert Whole number
                    string[] pr = price.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='planpricetab']//*[@class='card-table']//h3[text()='" + pr[0] + "']")).Displayed);
                    //Assert Decimals
                    if (pr.Length > 1)
                    {
                        Assert.IsTrue(driver.FindElement(By.XPath("//*[@id='planpricetab']//*[@class='card-table']//h3[text()='$']//sub[contains(text(),'" + pr[1] + "')]")).Displayed);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(price +" not Displayed as expected ");
                }
            }
     

        }

        public void ValidatePriceCards(string mode)
        {
            switch(mode)
            {
                case "Monthly":
                    List<string> monthlyPrices = new List<string> { "0", "199.98", "499.95" };
                    ValidatePriceOnCard(monthlyPrices);
                    ValidateCardTypes();
                    break;
                case "Quarterly":
                    List<string> quarterlyPrices = new List<string> { "0", "189.98", "474.95" };
                    ValidatePriceOnCard(quarterlyPrices);
                    ValidateCardTypes();
                    break;
                case "Half-yearly":
                    List<string> halfyearlyPrices = new List<string> { "0", "185.98", "464.95" };
                    ValidatePriceOnCard(halfyearlyPrices);
                    ValidateCardTypes();
                    break;
                case "Yearly":
                    List<string> yearlyPrices = new List<string> { "0", "179.98", "449.96" };
                    ValidatePriceOnCard(yearlyPrices);
                    ValidateCardTypes();
                    break;
                default:
                    Console.WriteLine("Invalid Pricing Mode");
                    break;
            }
        }

        public void ValidatePricing(List<string> pricingMode)
        {
            foreach(var mode in pricingMode)
            {
                try
                {
                    ClickOnPricingCircle(mode);
                    ValidatePriceCards(mode);

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


        }


        public void ValidateCardTypes()
        {
            IReadOnlyList<IWebElement> typeElements = driver.FindElements(By.XPath("//*[@id='planpricetab']//*[@class='card-table']//h5"));
            List<string> typeName = new List<string> { "Pay as you go" , "Business", "Premium" };
            int i = 0;
            foreach (var type in typeElements)
            {
                try
                {
                    string t = type.Text;
                    Assert.AreEqual(type.Text, typeName[i]);
                    i++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Type missing " + typeName[i]);
                }
               

            }
        }

        public void ValidateMenu(List<string> menuItems)
        {
            foreach(var menu in menuItems)
            {
                try
                {
                    Assert.AreEqual(menu, driver.FindElement(By.XPath("//*[@id='mobile-nav-menu']/following-sibling::ul/li/a[text()='" + menu + "']")).Text);
                    Console.WriteLine(menu + " menu is displayed");
                }

                catch(Exception e)
                {
                    Console.WriteLine(menu + " menu is Not displayed");
                }
            }
        }

    }
}