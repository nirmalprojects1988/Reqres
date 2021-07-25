using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using SeleniumExtras.PageObjects;
namespace Reqres
{
    public class Common_Operation
    {
        public static IWebDriver driver;
        public IWebDriver Init_Browser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }

        protected internal void Goto(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        protected internal void Close()
        {
            driver.Quit();
        }
        protected internal void Windowscroll(int x, int y)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(" + x + "," + y + ")", "");
        }
    }

    public class HomePage: Common_Operation
    {   
        
        private string gettextvalues;
        public HomePage()
        {
            Init_Browser();
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//li[@data-id='users']")]
        private IWebElement getListUserslnk;
        [FindsBy(How = How.XPath, Using = "//span[@class='url']")]
        private IWebElement requesttxt;

        [FindsBy(How = How.XPath, Using = "//span[@class='response-code']")]
        private IWebElement responsetxt;

        [FindsBy(How = How.XPath, Using = "//li[@data-id='post']")]
        private IWebElement postlink;

        
        public string VerifyGetListRequest()
        {
            getListUserslnk.Click();
            gettextvalues = requesttxt.Text;
            return gettextvalues;
        }
        public string VerifyGetListResponse()
        {
            getListUserslnk.Click();
            gettextvalues = responsetxt.Text;
            return gettextvalues;
        }

        public string VerifycreateRequest()
        {
            postlink.Click();
            gettextvalues= requesttxt.Text;
            return gettextvalues;
        }
        public string VerifycreateResponse()
        {
            postlink.Click();
            gettextvalues = responsetxt.Text;
            return gettextvalues;
        }

    }

    public class UnitTest1
    {
        readonly Common_Operation cop = new Common_Operation();
        const String  test_url = "https://reqres.in/";
        readonly HomePage obj = new HomePage();
        private string value;



        [Test]
        public void VerifyGetListUsers()
        {
            cop.Goto(test_url);
            cop.Windowscroll(0, 1000);
            value=obj.VerifyGetListRequest();
            Assert.That(value, Is.EqualTo("/api/users?page=2"));
        }
        [Test]
         public void VerifyGetListUsersResponse()
        {
            cop.Goto(test_url);
            cop.Windowscroll(0, 1000);
            value = obj.VerifyGetListResponse();
            Assert.That(value, Is.EqualTo("200"));

        }
        [Test]
        public void VerifyCreateRequest()
        {
            cop.Goto(test_url);
            cop.Windowscroll(0, 1500);
            value = obj.VerifycreateRequest();
            Assert.That(value, Is.EqualTo("/api/users"));
        }
        [Test]
        public void VerifyCreateResponse()
        {
            cop.Goto(test_url);
            cop.Windowscroll(0, 1500);
            value = obj.VerifycreateResponse();
            Assert.That(value, Is.EqualTo("200"));
        }

      [TearDown]
        public void close_Browser()
        {
            cop.Close();
        }
    }
}
