using NUnit.Framework;
using System;
using System.Collections.Generic;
using Library.Model;
using Library.Utilities;

namespace Test.Library.Model
{
    [TestFixture()]
    public class PlayHandTest
    {
        [Test()]
        public void Test_PlayHand()
        {
            List<Card> cardsA = new List<Card>
            {
                new Card(CardRank.Ace, CardSuit.HEART),
                new Card(CardRank.Four, CardSuit.SPADE),
            };
            List<Card> cardsB = new List<Card>
            {
                new Card(CardRank.King, CardSuit.CLUB),
            };

            PlayerHand playerHand = new PlayerHand(cardsA);
            playerHand.HandRank = HandRank.HighCard;
            Assert.AreEqual(playerHand.Cards.Count, 2);
            Assert.IsTrue(playerHand.HandRank == HandRank.HighCard);

            playerHand.Cards = cardsB;
            playerHand.HandRank = HandRank.ThreeOfAKind;
            Assert.AreEqual(playerHand.Cards.Count, 1);
            Assert.IsTrue(playerHand.HandRank == HandRank.ThreeOfAKind);
        }

        [Test()]
        public void Test_CompareTo()
        {
            List<Card> cardsA = new List<Card>
            {
                new Card(CardRank.Ace, CardSuit.HEART),
                new Card(CardRank.Four, CardSuit.SPADE),
            };
            List<Card> cardsB = new List<Card>
            {
                new Card(CardRank.King, CardSuit.CLUB),
                new Card(CardRank.Four, CardSuit.HEART),
            };
            PlayerHand playerHandA = new PlayerHand(cardsA);
            PlayerHand playerHandB = new PlayerHand(cardsB);

            Assert.IsTrue(playerHandA.CompareTo(playerHandB) == 1);
            Assert.IsTrue(playerHandA.CompareTo(playerHandA) == 0);
            Assert.IsTrue(playerHandB.CompareTo(playerHandA) == -1);
        }
    }
}
