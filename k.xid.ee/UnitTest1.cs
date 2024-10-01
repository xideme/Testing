using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace k.xid.ee.Tests
{
    [TestFixture]
    public class ContactFormTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\xideme\Testing\k.xid.ee\driver");
            driver.Navigate().GoToUrl("https://k.xid.ee");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);  // Ensure full page load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));  
        }



        [Test]
        public void SubmitContactFormTest()
        {
            try
            {
                    // Find and click the "Kontakt" button
                    IWebElement kontaktButton = wait.Until(driver => driver.FindElement(By.LinkText("Kontakt")));
                    kontaktButton.Click();

                    // Wait for the form to appear
                    wait.Until(driver => driver.FindElement(By.Name("menu-655")));

                    // Fill out the form
                    IWebElement countryDropdown = driver.FindElement(By.Name("menu-655"));
                    SelectElement selectCountry = new SelectElement(countryDropdown);
                    selectCountry.SelectByText("Eesti");
                    Thread.Sleep(1000);

                    IWebElement emailField = driver.FindElement(By.Name("your-email"));
                    emailField.SendKeys("email@xid.ee");
                    Thread.Sleep(1000);

                    IWebElement subjectField = driver.FindElement(By.Name("your-subject"));
                    subjectField.SendKeys("Tere");
                    Thread.Sleep(1000);

                    IWebElement messageField = driver.FindElement(By.Name("your-message"));
                    messageField.SendKeys("Kuidas laheb");

                    // Submit the form
                    IWebElement submitButton = driver.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit.has-spinner"));
                    submitButton.Click();
                    Thread.Sleep(3000);

                    // Wait for the success message to appear
                    IWebElement successMessage = wait.Until(driver =>
                        driver.FindElement(By.ClassName("wpcf7-response-output"))
                    );

                    Assert.IsTrue(successMessage.Text.Contains("Täname sõnumi eest. See saadeti teele."),
                                  $"Unexpected success message: {successMessage.Text}");

                    Console.WriteLine("Test Success: " + successMessage.Text);
            }
            catch (WebDriverTimeoutException ex)
            {
                    Assert.Fail($"Test failed due to timeout: {ex.Message}");
            }
            catch (NoSuchElementException ex)
            {
                    Assert.Fail($"Test failed due to missing element: {ex.Message}");
            }
        }

        [Test]
        public void FaqButtonShouldExistTest()
        {
            // if the FAQ button exists
            IWebElement faqButton = null;

                try
                {
                    // find the FAQ button
                    faqButton = driver.FindElement(By.LinkText("FAQ"));
                }
                catch (NoSuchElementException)
                {
                    // If the button is not found
                    Assert.Fail("The 'FAQ' button was not found.");
                }

            // If the button was found
            Assert.IsTrue(faqButton.Displayed, "The 'FAQ' button is displayed.");
            Console.WriteLine("The 'FAQ' button exists and is displayed.");
        }


        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
