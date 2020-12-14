using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;

namespace LoginBDD.Steps
{
    [Binding]
    public class RegistrationSteps
    {
        private IWebDriver _driver;
        private readonly string _rMainPageUrl = "http://localhost:4200/";

        [Given(@"Launch Firefox")]
        public void GivenLaunchFirefox()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"Y:\Univer\Проекты\GameSalesWebsite\backend\GameSalesApi\LoginBDD\Drivers", "geckodriver.exe");
            _driver = new FirefoxDriver(service);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
        }

        [Given(@"Navigate to Web Frontend")]
        public void GivenNavigateToWebFrontend()
        {
            _driver.Navigate().GoToUrl(_rMainPageUrl);
        }

        [Then(@"click on Login button")]
        public void ThenClickOnLoginButton()
        {
            _driver.FindElement(By.Id("login")).Click();
        }

        [When(@"Enter ""(.*)"" for email")]
        public void WhenEnterForEmail(string p0)
        {
            _driver.FindElement(By.Id("emailInput")).SendKeys(p0);
        }
        
        [When(@"Enter ""(.*)"" for username")]
        public void WhenEnterForUsername(string p0)
        {
            _driver.FindElement(By.Id("userNameInput")).SendKeys(p0);
        }
        
        [When(@"Enter ""(.*)"" for first name")]
        public void WhenEnterForFirstName(string p0)
        {
            _driver.FindElement(By.Id("firstNameInput")).SendKeys(p0);
        }
        
        [When(@"Enter ""(.*)"" for last name")]
        public void WhenEnterForLastName(string p0)
        {
            _driver.FindElement(By.Id("lastNameInput")).SendKeys(p0);
        }
        
        [When(@"Enter ""(.*)"" for password")]
        public void WhenEnterForPassword(string p0)
        {
            _driver.FindElement(By.Id("passwordInput")).SendKeys(p0);
        }
        
        [When(@"Enter ""(.*)"" for confirm password")]
        public void WhenEnterForConfirmPassword(string p0)
        {
            _driver.FindElement(By.Id("passwordConfirmInput")).SendKeys(p0);
        }
        
        [When(@"Click on Register button")]
        public void WhenClickOnRegisterButton()
        {
            _driver.FindElement(By.Id("registerSubmit")).Click();
        }

        [When(@"Click on Login button")]
        public void WhenClickOnLoginButton()
        {
            _driver.FindElement(By.Id("login")).Click();
        }

        [Then(@"click on Register button")]
        public void ThenClickOnRegisterButton()
        {
            _driver.FindElement(By.Id("registerButton")).Click();
        }
        
        [Then(@"Register sucesfull")]
        public void ThenRegisterSucesfull()
        {
            _driver.Url.Should().Be($"{_rMainPageUrl}login");

            _driver.Close();
        }

        [When(@"Enter valid email")]
        public void WhenEnterValidEmail()
        {
            string testEmail = $"{Guid.NewGuid().ToString("N")}@test.com";

            _driver.FindElement(By.Id("emailInput")).SendKeys(testEmail);
        }

        [When(@"Enter valid username")]
        public void WhenEnterValidUsername()
        {
            string testEmail = $"{Guid.NewGuid().ToString("N").Substring(0, 10)}userName";

            _driver.FindElement(By.Id("userNameInput")).SendKeys(testEmail);
        }

        [When(@"Enter existed ""(.*)"" for email")]
        public void WhenEnterExistedForEmail(string p0)
        {
            _driver.FindElement(By.Id("emailInput")).SendKeys(p0);
        }

        [When(@"Enter existed ""(.*)"" for username")]
        public void WhenEnterExistedForUsername(string p0)
        {
            _driver.FindElement(By.Id("userNameInput")).SendKeys(p0);
        }

        [Then(@"message under password ""(.*)""")]
        public void ThenMessageUnderPassword(string p0)
        {
            var error = _driver.FindElement(By.Id("passErrorUpperLowerCasesDigit"));

            error.Should().NotBeNull();
            error.Displayed.Should().BeTrue();
            error.Enabled.Should().BeTrue();
            error.Text.Should().Be(p0);

            _driver.Close();
        }

        [Then(@"message ""(.*)"" and be on register page")]
        public void ThenMessageAndBeOnRegisterPage(string p0)
        {
            var alert_win = _driver.SwitchTo().Alert();

            p0.Should().Be(alert_win.Text.Trim());

            alert_win.Accept();

            _driver.Url.Should().Be($"{_rMainPageUrl}register");

            _driver.Close();
        }
    }
}
