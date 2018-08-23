using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Faker;

namespace EEBookingForm
{
    public class eeBookingForm
    {

        IWebDriver driver = new ChromeDriver();


        [Test]

        public void CreateBooking()
        {            

            //Open The browser, Maximise the Window, and navigate to Target URL then Assert the Title
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://hotel-test.equalexperts.io/");

            Assert.AreEqual("Hotel booking form", driver.Title);


            //Populate the FirstName and LastName fields with randomly generated values from Faker
            driver.FindElement(By.Id("firstname")).SendKeys(Name.First());
            driver.FindElement(By.Id("lastname")).SendKeys(Name.Last());

            //Generate a random TotalPrice from -10000 to +10000
            var randomprice = new System.Random();
            int totalPrice = randomprice.Next(-10000, 10000);
            driver.FindElement(By.Id("totalprice")).SendKeys(totalPrice.ToString());

            //Select Random DepositPaid Option
            driver.FindElement(By.Id("depositpaid")).FindElement(By.XPath(".//option[contains(text(),'true')]")).Click();


            //Select the Date Picker and Generate a Random Date to be used as the Checkin Date
            driver.FindElement(By.Id("checkin")).Click();
            var randomDays = new System.Random();
            var checkinDate = System.DateTime.Today.AddDays(randomDays.Next(1, 365));

            //Compare the generated Checkin Date against Todays Date and select the Check in Month from the Date Picker

            var currentMonth = System.DateTime.Today.Month;
            var checkinMonthCount = System.DateTime.Today.Month;

            do
            {
                driver.FindElement(By.XPath("//span[@class='ui-icon ui-icon-circle-triangle-e']")).Click();

                if (checkinMonthCount != 12)
                {
                    checkinMonthCount++;
                }
                else
                {
                    checkinMonthCount = 1;
                }

            }
            while (checkinDate.Month != checkinMonthCount);


            driver.FindElement(By.LinkText(checkinDate.Day.ToString())).Click();

            //Select Check Out Date using a random number of days (booking duration) from 1-30 from the checkin Date

            driver.FindElement(By.Id("checkout")).Click();
            var checkOutDate = checkinDate.AddDays(randomDays.Next(1, 30));

            var checkOutMonthCount = System.DateTime.Today.Month;

            do
            {

                driver.FindElement(By.XPath("//span[@class='ui-icon ui-icon-circle-triangle-e']")).Click();

                if (checkOutMonthCount != 12)
                {
                    checkOutMonthCount++;
                }
                else
                {
                    checkOutMonthCount = 1;
                }


            }
            while (checkOutDate.Month != checkOutMonthCount);

            driver.FindElement(By.LinkText(checkOutDate.Day.ToString())).Click();

            //Submit the booking

            driver.FindElement(By.CssSelector("input[value=' Save ']")).Click();


            //log the output of the Booking Dates
            System.Console.WriteLine("Check In Date = " + checkinDate);
            System.Console.WriteLine("Check In Date Days = " + checkinDate.Day);
            System.Console.WriteLine("Check In Date Month = " + checkinDate.Month);
            System.Console.WriteLine("Check In Date Year = " + checkinDate.Year);
            System.Console.WriteLine("Check Out Date = " + checkOutDate);
            System.Console.WriteLine("Check Out Date Days = " + checkOutDate.Day);
            System.Console.WriteLine("Check Out Date Month = " + checkOutDate.Month);
            System.Console.WriteLine("Check Out Date Year = " + checkOutDate.Year);
            System.Console.WriteLine("Current Date = " + System.DateTime.Today);

            //driver.Close();
            //driver.Quit();

        }

    }
}
