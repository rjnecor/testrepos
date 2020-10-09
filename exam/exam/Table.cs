using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;


namespace exam
{
    public class Table
    {
        public List<Row> Rows;
        public string[] Headers;
        private readonly IWebDriver driver;
        public int RowCount;
        public int CellCount;

        public Table(IWebDriver driver, string tablexpath = "//table")
        {
            this.driver = driver;
            ScrapeTable(tablexpath);
        }

        public void ScrapeTable(string tablexpath = "//table")
        {
            RowCount = driver.FindElements(By.XPath(tablexpath + "/tbody/tr")).Count;
            CellCount = driver.FindElements(By.XPath(tablexpath + "/tbody/tr[1]/td")).Count;
            List<Row> rows = new List<Row>();
            for (int i = 1; i <= RowCount; i++)
            {
                rows.Add(new Row(
                    driver.FindElements(By.XPath(tablexpath + "/tbody/tr[" + i + "]/td"))
                    .Select(d =>d.Text)                    
                    .ToArray()));
            }
            Rows = rows;
        }
    }

    public class Row
    {
        public string[] Cell;
        public Row(string[] cells)
        {
            Cell = cells;
        }
    }
}
