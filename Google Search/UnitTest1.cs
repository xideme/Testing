using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

class GoogleSearchWeather
{
    static void Main(string[] args)
    {
        // Initialize ChromeDriver (Make sure ChromeDriver is installed and added to the PATH)
        WebDriver driver = new ChromeDriver(@"C:\Users\opilane\source\repos\xideme\Testing\Google Search\driver");


        try
        {
            driver.Navigate().GoToUrl("https://www.google.ee");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                // Locate the "Nõustu kõigiga" button by its text and click it
                IWebElement acceptAllButton = wait.Until(drv => drv.FindElement(By.XPath("//button[.='Nõustu kõigiga']")));
                acceptAllButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Nõustu kõigiga button not found or took too long to appear.");
            }

            IWebElement searchBox = driver.FindElement(By.Name("q"));

            searchBox.SendKeys("Weather");

            searchBox.Submit();

            System.Threading.Thread.Sleep(3000); // Wait for 3 seconds
        }
        finally
        {
            // Close the browser
            driver.Quit();
        }
    }
}

