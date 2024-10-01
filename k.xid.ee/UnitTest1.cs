using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace k.xid.ee
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up ChromeDriver
            WebDriver driver = new ChromeDriver(@"C:\Users\opilane\source\repos\xideme\Testing\k.xid.ee\driver");

            try
            {
                // Navigate to the website
                driver.Navigate().GoToUrl("https://k.xid.ee");

                // Set up wait (optional, can be useful if you need to wait for page elements to load)
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                System.Threading.Thread.Sleep(3000); 
                // Try to find and click the "Kontakt" button
                try
                {
                    IWebElement kontaktButton = driver.FindElement(By.LinkText("Kontakt"));
                    Console.WriteLine("The 'Kontakt' button exists.");

                    // Click the "Kontakt" button
                    kontaktButton.Click();
                    Console.WriteLine("Clicked the 'Kontakt' button.");

                    // Wait for the form to appear
                    wait.Until(driver => driver.FindElement(By.Name("menu-655"))); // Wait for the dropdown to be present

                    // Fill out the form
                    // Select "Vali oma riik"
                    IWebElement countryDropdown = driver.FindElement(By.Name("menu-655"));
                    SelectElement selectCountry = new SelectElement(countryDropdown);
                    selectCountry.SelectByText("Eesti"); // Select Eesti from the dropdown

                    // Fill in the "Email" field
                    IWebElement emailField = driver.FindElement(By.Name("your-email")); // Access the email field
                    emailField.SendKeys("your-email@example.com");

                    // Fill in the "Teema" field (subject)
                    IWebElement subjectField = driver.FindElement(By.Name("your-subject")); // Access the subject field
                    subjectField.SendKeys("This is a test subject");

                    // Fill in the optional "Kiri" field (message)
                    IWebElement messageField = driver.FindElement(By.Name("your-message")); // Access the message field (textarea)
                    messageField.SendKeys("This is a test message for the contact form.");

                    Console.WriteLine("Form filled out successfully.");

                    // Click the submit button
                    IWebElement submitButton = driver.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit.has-spinner"));
                    submitButton.Click();
                    Console.WriteLine("Clicked the submit button.");

                    // Wait for the success message to appear
                    wait.Until(driver => driver.FindElement(By.ClassName("wpcf7-response-output"))); // Wait for the success message to be present

                    // Verify the success message
                    IWebElement successMessage = driver.FindElement(By.ClassName("wpcf7-response-output")); // Access the success message div
                    if (successMessage.Displayed && successMessage.Text.Contains("Täname sõnumi eest. See saadeti teele."))
                    {
                        Console.WriteLine("Success: " + successMessage.Text); // Show the message in console
                    }
                    System.Threading.Thread.Sleep(20000); // Wait for 5 seconds before quitting
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("The 'Kontakt' button or form elements do not exist.");
                    System.Threading.Thread.Sleep(5000); // Wait for 5 seconds before quitting
                }
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }
    }
}
