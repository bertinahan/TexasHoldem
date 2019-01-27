using NUnit.Framework;
using System;
using System.Collections.Generic;
using Library.Model;
using Library.Utilities;
using Library.Exceptions;

namespace Test.Library.Utilities
{
    [TestFixture()]
    public class AnalyserTest
    {
        [Test()]
        public void Test_Analyser()
        {
            Analyser analyser = new Analyser();
            PlayerHand playerHand = new PlayerHand();
            Assert.Throws(typeof(InvalidPokerRuleException), delegate {
                analyser.SetHandRank(playerHand);
            });
        }

        [Test()]
        public void Test_Flush()
        {
            List<HandRank> rules = new List<HandRank>
            {
                HandRank.OnePair,
                HandRank.ThreeOfAKind,
                HandRank.Flush,
                HandRank.HighCard,
            };
            Analyser analyser = new Analyser(rules);
            List<Card> cards = new List<Card>
            {
                new Card(CardRank.Five, CardSuit.DIAMOND),
                new Card(CardRank.Six, CardSuit.DIAMOND),
                new Card(CardRank.Ace, CardSuit.DIAMOND),
                new Card(CardRank.Jack, CardSuit.DIAMOND),
                new Card(CardRank.Queen, CardSuit.DIAMOND),
            };
            CardRank[] expected = {CardRank.Ace, CardRank.Queen, CardRank.Jack,
            CardRank.Six, CardRank.Five};

            PlayerHand playerHand = new PlayerHand(cards);
            analyser.SetHandRank(playerHand);
            Assert.IsTrue(playerHand.HandRank == HandRank.Flush);
            for (int i = 0; i < playerHand.Cards.Count; i++)
            {
                Assert.IsTrue(playerHand.Cards[i].Rank == expected[i]);
            }
        }

        [Test()]
        public void Test_OnePair()
        {
            List<HandRank> rules = new List<HandRank>
            {
                HandRank.ThreeOfAKind,
                HandRank.OnePair,
                HandRank.Flush,
                HandRank.HighCard,
            };
            Analyser analyser = new Analyser(rules);
            List<Card> cards = new List<Card>
            {
                new Card(CardRank.Five, CardSuit.SPADE),
                new Card(CardRank.Six, CardSuit.DIAMOND),
                new Card(CardRank.Ace, CardSuit.DIAMOND),
                new Card(CardRank.Five, CardSuit.CLUB),
                new Card(CardRank.Queen, CardSuit.DIAMOND),
            };
            CardRank[] expected = {CardRank.Five, CardRank.Five, CardRank.Ace,
            CardRank.Queen, CardRank.Six};

            PlayerHand playerHand = new PlayerHand(cards);
            analyser.SetHandRank(playerHand);
            Assert.IsTrue(playerHand.HandRank == HandRank.OnePair);
            for (int i = 0; i < playerHand.Cards.Count; i++)
            {
                Assert.IsTrue(playerHand.Cards[i].Rank == expected[i]);
            }
        }

        [Test()]
        public void Test_ThreeOfAKind()
        {
            List<HandRank> rules = new List<HandRank>
            {
                HandRank.ThreeOfAKind,
                HandRank.OnePair,
                HandRank.Flush,
                HandRank.HighCard,
            };
            Analyser analyser = new Analyser(rules);
            List<Card> cards = new List<Card>
            {
                new Card(CardRank.Five, CardSuit.SPADE),
                new Card(CardRank.Six, CardSuit.DIAMOND),
                new Card(CardRank.Five, CardSuit.HEART),
                new Card(CardRank.Five, CardSuit.CLUB),
                new Card(CardRank.Queen, CardSuit.DIAMOND),
            };
            CardRank[] expected = {CardRank.Five, CardRank.Five, CardRank.Five,
            CardRank.Queen, CardRank.Six};

            PlayerHand playerHand = new PlayerHand(cards);
            analyser.SetHandRank(playerHand);
            Assert.IsTrue(playerHand.HandRank == HandRank.ThreeOfAKind);
            for (int i = 0; i < playerHand.Cards.Count; i++)
            {
                Assert.IsTrue(playerHand.Cards[i].Rank == expected[i]);
            }
        }

        [Test()]
        public void Test_HighCard()
        {
            List<HandRank> rules = new List<HandRank>
            {
                HandRank.ThreeOfAKind,
                HandRank.OnePair,
                HandRank.Flush,
                HandRank.HighCard,
            };
            Analyser analyser = new Analyser(rules);
            List<Card> cards = new List<Card>
            {
                new Card(CardRank.Ace, CardSuit.SPADE),
                new Card(CardRank.Six, CardSuit.DIAMOND),
                new Card(CardRank.Jack, CardSuit.HEART),
                new Card(CardRank.Five, CardSuit.CLUB),
                new Card(CardRank.Queen, CardSuit.DIAMOND),
            };
            CardRank[] expected = {CardRank.Ace, CardRank.Queen, CardRank.Jack,
            CardRank.Six, CardRank.Five};

            PlayerHand playerHand = new PlayerHand(cards);
            analyser.SetHandRank(playerHand);
            Assert.IsTrue(playerHand.HandRank == HandRank.HighCard);
            for (int i = 0; i < playerHand.Cards.Count; i++)
            {
                Assert.IsTrue(playerHand.Cards[i].Rank == expected[i]);
            }
        }
    }

    
}
