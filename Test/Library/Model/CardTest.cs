using NUnit.Framework;
using System;
using Library.Model;
using Library.Utilities;

namespace Test.Library.Model
{
    [TestFixture()]
    public class CardTest
    {
        [Test()]
        public void Test_Card()
        {
            Card card = new Card(CardRank.Ace, CardSuit.CLUB);
            Assert.IsTrue(card.Rank == CardRank.Ace);
            Assert.IsTrue(card.Suit == CardSuit.CLUB);
        }

        [Test()]
        public void Test_CompareTo()
        {
            Card cardA = new Card(CardRank.Ace, CardSuit.CLUB);
            Card cardB = new Card(CardRank.Eight, CardSuit.DIAMOND);
            Assert.IsTrue(cardA.CompareTo(cardB) == 1);
            Assert.IsTrue(cardA.CompareTo(null) == 1);
            Assert.IsTrue(cardA.CompareTo(cardA) == 0);
            Assert.IsTrue(cardB.CompareTo(cardA) == -1);
        }

        [Test()]
        public void Test_ToString()
        {
            Card card = new Card(CardRank.Ace, CardSuit.CLUB);
            Assert.IsTrue(card.ToString().Equals("Ace CLUB"));
        }
    }
}
