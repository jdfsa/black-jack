using Jdfs.BlackJack.Api.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jdfs.BlackJack.Test
{
    [TestClass]
    public class ApplicationCardsTest
    {
        [TestMethod]
        public void GetCards()
        {
            var cards = ApplicationCards.GetCards();
            Assert.IsNotNull(cards, "GetCards() should not return null");
            Assert.AreEqual<int>(52, cards.Count, "Pack should have 52 cards");
        }
    }
}
