using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;

namespace LoginBDD.Steps
{
    [Binding]
    public class LogInSteps
    {
        private IWebDriver _driver;
        private readonly string _rMainPageUrl = "http://localhost:4200/";

        [Given(@"LogIn Launch Firefox")]
        public void GivenLogInLaunchFirefox()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"Y:\Univer\Проекты\GameSalesWebsite\backend\GameSalesApi\LoginBDD\Drivers", "geckodriver.exe");
            _driver = new FirefoxDriver(service);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
        }

        [Given(@"LogIn Navigate to Web Frontend")]
        public void GivenLogInNavigateToWebFrontend()
        {
            _driver.Navigate().GoToUrl(_rMainPageUrl);
        }

        [When(@"Enter ""(.*)"" for login")]
        public void WhenEnterForLogin(string p0)
        {
            _driver.FindElement(By.Id("loginEmail")).SendKeys(p0);
        }

        [When(@"""(.*)"" for password")]
        public void WhenForPassword(string p0)
        {
            _driver.FindElement(By.Id("loginPassword")).SendKeys(p0);
        }

        [When(@"Click on LogIn button")]
        public void WhenClickOnLogInButton()
        {
            _driver.FindElement(By.Id("loginSubmit")).Click();
        }

        [Then(@"click on Login button for LogIn")]
        public void ThenClickOnLoginButtonForLogIn()
        {
            _driver.FindElement(By.Id("login")).Click();
        }
        
        [Then(@"login sucesfull")]
        public void ThenLoginSucesfull()
        {
            _driver.Url.Should().Be(_rMainPageUrl);

            var personalCabButton = _driver.FindElement(By.Id("personalCab"));

            personalCabButton.Should().NotBeNull();
            personalCabButton.Displayed.Should().BeTrue();

            _driver.Close();
        }

        [Then(@"message ""(.*)""")]
        public void ThenMessage(string p0)
        {
            var alert_win = _driver.SwitchTo().Alert();

            p0.Should().Be(alert_win.Text.Trim());

            alert_win.Accept();

            _driver.Url.Should().Be($"{_rMainPageUrl}login");

            _driver.Close();
        }
    }
}
