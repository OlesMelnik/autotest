using Atata;
using NUnit.Framework;
using System;

namespace AtataUITests1
{
    public class SampleTests : UITestFixture
    {
        [Test]
        public void SampleTest()
        {
            //Go.To<OrdinaryPage>()
            //    .PageTitle.Should.Contain("Atata");

            string product = "apple macbook 13";
            char[] ch = { '₴', ' ' };

            Go.To<Page1>()
                .Search.Set(product)
                .SearchButton.Click();


            string priceFirst = Go.To<Page1>().Price.Get().Trim(ch);

            Go.To<Page1>()
                .Good.Click();

            string priceSecond = Go.To<Page2>().Price.Get().Trim(ch);

            Go.ToUrl(String.Format("https://rozetka.com.ua/search/?text={0}",
                         product));


            string priceThird = Go.To<Page1>().Price.Get().Trim(ch);

            Assert.AreEqual(priceFirst, priceSecond);
            Assert.AreEqual(priceFirst, priceThird);
        }
    }
}
