using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace exam
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenerateTestData()
        {
            Uri uri = new Uri(@"http://localhost:9999/statements");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StringContent httpcontent;
            int maxusers = 10;
            Account tempacct;
            for (int i = 0; i < maxusers; i++)
            {
                tempacct = new Account();
                httpcontent = new StringContent(tempacct.GeneratePostBody(), Encoding.UTF8, "application/json");
                var postresponse = client.PostAsync(uri, httpcontent);
                Console.WriteLine("Generated data: " + tempacct.AccountId + " / " + tempacct.Amount + " / " + tempacct.DateCreated +
                    "\r\nresponse status: " + postresponse.Result.StatusCode);
            }
        }

        [TestMethod]
        public void TestCalculation()
        {
            IWebDriver driver = new WebDirver().Driver;
            //Access website
            driver.Navigate().GoToUrl(@"http://localhost:9999/statements");
            //Search
            driver.FindElement(By.XPath("//button[text()='Search']")).Click();

            //ScrapeTable
            Table table = new Table(driver);

            float sum = 0;
            //Get sum
            foreach (Row row in table.Rows)
            {
                sum += float.Parse(row.Cell[1].Replace(" EUR", ""));
            }
            //validate sum
            string wholetext = driver.FindElement(By.ClassName("col-sm")).Text;
            Regex pattern = new Regex(@"Balance after (?<amount>[-]?\d+\.\d+) EUR");
            Match match = pattern.Match(wholetext);
            float displayedvalue = float.Parse(match.Groups["amount"].Value);
            driver.Quit();
            Assert.IsTrue(sum == displayedvalue, "Display: " + displayedvalue + "\r\ncomputed: " + sum);
        }
    }
}
