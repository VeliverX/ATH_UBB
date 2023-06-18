using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReservTest
{
    public class UnitTest1
    {
        [Fact]
        public void ReservMain()
        {
            ChromeDriver driver = new ChromeDriver();//Google Chrome
            driver.Manage().Window.Maximize(); 
            driver.Navigate().GoToUrl("https://localhost:7063"); // Localhost dla mojego przypadku

            //Przjescie do rezerwacji
            driver.FindElement(By.PartialLinkText("Vehicles")).Click();
            driver.FindElement(By.PartialLinkText("Details")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();

            //Po kliknieciu rezerwacji zalogwanie na konto user
            driver.FindElement(By.Id("Input_Email")).SendKeys("nikodem@ath.edu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Haslo123!");
            driver.FindElement(By.Id("login-submit")).Click();

            //formularz rezerwacji
            var StartDay = driver.FindElement(By.Id("StartDay"));
            StartDay.SendKeys("2102023");
            StartDay.SendKeys(Keys.Tab);
            StartDay.SendKeys("1000AM");
            var EndDay = driver.FindElement(By.Id("EndDay"));
            EndDay.SendKeys("3102023");
            EndDay.SendKeys(Keys.Tab);
            EndDay.SendKeys("1000AM");
            driver.FindElement(By.Id("create")).Click();

            //Sprawdzenie czy rezerwacja przeszłą poprzez znikniecie przycisku do rezerwacji dla usera
            driver.FindElement(By.PartialLinkText("Strona Główna")).Click(); 
            driver.FindElement(By.PartialLinkText("Vehicles")).Click();
            driver.FindElement(By.PartialLinkText("Details")).Click();
            Assert.Empty(driver.FindElements(By.PartialLinkText("Zarezerwuj")));

            //Wylogowanie a nastepnie zalogwanie na konto admina
            driver.FindElement(By.Id("logout")).Click();
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@ath.edu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Haslo123!");
            driver.FindElement(By.Id("login-submit")).Click();

            //przejscie do listy i zakonczenie rezerwacji
            driver.FindElement(By.PartialLinkText("Vehicles")).Click();
            driver.FindElement(By.PartialLinkText("Details")).Click();
            driver.FindElement(By.Id("delete")).Click();
            
            //ostateczne sprawdzenie czy pojazd jest znów mozliwy do wynajęcia 
            driver.FindElement(By.PartialLinkText("Details")).Click();
            driver.FindElements(By.PartialLinkText("Zarezerwuj"));

            driver.Quit();

        }
    }
}