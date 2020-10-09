using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace exam
{
    public class WebDirver
    {
        public IWebDriver Driver;

        public WebDirver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            Driver = new ChromeDriver(options);
        }
    }
}
