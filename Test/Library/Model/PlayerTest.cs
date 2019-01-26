using NUnit.Framework;
using System;
using System.Collections.Generic;
using Library.Model;
using Library.Utilities;

namespace Test.Library.Model
{
    [TestFixture()]
    public class PlayerTest
    {
        [Test()]
        public void Test_Player()
        {
            Player player = new Player("Alice");
            Assert.AreEqual(player.Name, "Alice");

            player = new Player();
            Assert.AreEqual(player.Name, "");
        }

        [Test()]
        public void Test_Set_PlayHand()
        {
            Player player = new Player("Alice");
            List<Card> cards = new List<Card>
            {
                new Card(CardRank.Ace, CardSuit.DIAMOND)
            };
            PlayerHand playerHand = new PlayerHand(cards);
            player.PlayerHand = playerHand;
            Assert.AreEqual(player.PlayerHand.Cards.Count, 1);
        }
    }
}
