using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;

namespace HerokuLoginTests
{
    internal class Program
    {
        // Create a reference for Chrome browser
        IWebDriver driver = new ChromeDriver();
        static void Main(string[] args)
        {
        }

        [SetUp]
        public void Initialize()
        {
            // Go to Login Page
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        }

        [Test]
        public void ValidLoginTest()
        {
            // Maximize the browser
            driver.Manage().Window.Maximize();

            // Find the username field and enter valid username
            IWebElement usernameField = driver.FindElement(By.Id("username"));
            usernameField.SendKeys("tomsmith");

            // Find the password field and enter valid password
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("SuperSecretPassword!");

            // Click the login button
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Verify successful login by checking the message or URL
            IWebElement successMessage = driver.FindElement(By.CssSelector(".flash.success"));
            string messageText = successMessage.Text;

            // Simple check to confirm success
            if (!messageText.Contains("You logged into a secure area!"))
            {
                throw new Exception("Login failed: success message not found.");
            }
        }

        [Test]
        public void InvalidLoginTest()
        {
            driver.Manage().Window.Maximize();

            // Enter invalid credentials
            IWebElement usernameField = driver.FindElement(By.Id("username"));
            usernameField.SendKeys("wronguser");

            IWebElement passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("wrongpassword");

            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            IWebElement errorMessage = driver.FindElement(By.CssSelector(".flash.error"));
            string errorText = errorMessage.Text;

            if (!errorText.Contains("Your username is invalid!"))
            {
                throw new Exception("Invalid login test failed: error message not found.");
            }
        }

        [TearDown]
        public void CloseTest()
        {
            // Close the browser
            driver.Quit();
        }
    }
}
