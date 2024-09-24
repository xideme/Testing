using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;


namespace SeleniumTest;
public static class UnitTest1
{
    public static void Main()
    {
        IWebDriver driver = new ChromeDriver();

        driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");

        var title = driver.Title;

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000000);

        var textBox = driver.FindElement(By.Name("my-text"));
        var submitButton = driver.FindElement(By.TagName("button"));

        textBox.SendKeys("Selenium");
        submitButton.Click();

        var message = driver.FindElement(By.Id("message"));
        var value = message.Text;

        driver.Quit();
    }
}