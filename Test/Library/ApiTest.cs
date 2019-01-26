using NUnit.Framework;
using System;
using System.Collections.Generic;
using Library;
using Library.Exceptions;
using Library.Model;
using Library.Utilities;


namespace Test.Library
{
    [TestFixture()]
    public class ApiTest
    {
        [SetUp]
        public void ResetGame() 
        {
            Api.ResetGame();
        } 

        [Test()]
        public void Test_RegisterPlayer_ThrowException()
        {
            string[] invalidPlayerName = { "", null, " ", "   " };
            foreach (string invalidName in invalidPlayerName)
            {
                Assert.Throws(typeof(InvalidPlayerException), delegate
                {
                    Api.RegisterPlayer(invalidName);
                });
            }
        }

        public void Test_RegisterPlayer_Success()
        {
            string[] validPlayerName = { "s", "Joe", "blue" };
            foreach (string validName in validPlayerName)
            {
                Assert.AreEqual(Api.RegisterPlayer(validName).Name, validName);

            }
        }

        [Test()]
        public void Test_RegisterPlayerHand_ThrowException()
        {
            string[] invalidCards = { " ", null, "", ",,,,", "12,ah,2h,3h,4h"
            , "5d,3s,7d,5d,8h", "ah", "3c,5c,6c,7c", "1h, ,4h,5h,6h",
            ",    5s ,4h,5h,6h"};
            foreach (string invalidCard in invalidCards)
            {
                Assert.Throws(typeof(InvalidCardException), delegate {
                    Api.RegisterPlayerHand(invalidCard);
                });
            }

            Api.ResetGame();
            Api.RegisterPlayerHand("ah,2h,3h,4h,5h");
            Assert.Throws(typeof(InvalidCardException), delegate {
                Api.RegisterPlayerHand("5s,3s,ah,4s,8d");
            });
        }

        [Test()]
        public void Test_RegisterPlayerHand_Sucess()
        {
            string[] validCards = { "as,ks,qh,3h,10c", "2h,5h,8h,ad,qd",
            "8c,3d,    5d, 7H,    10d    " };
            string[] expectedCards = { "Ace SPADE", "King SPADE", "Queen HEART",
            "Three HEART", "Ten CLUB" };
            List<PlayerHand> playerHands = new List<PlayerHand>();

            Api.ResetGame();

            for (int i = 0; i < validCards.Length; i++)
            {
                playerHands.Add(Api.RegisterPlayerHand(validCards[i]));
            }

            List<Card> player1Cards = playerHands[0].Cards;
            for (int i = 0; i < player1Cards.Count; i++)
            {
                Assert.AreEqual(player1Cards[i].ToString(), expectedCards[i]);
            }
        }

        [Test()]
        public void Test_RegisterRules_ThrowException()
        {
            HandRank invalidHandRank = (HandRank)Enum.Parse(typeof(HandRank), "15");
            Assert.Throws(typeof(InvalidPokerRuleException), delegate {
                Api.RegisterRules(invalidHandRank);
            });
            List<Player> players = new List<Player>();
            Player alice = Api.RegisterPlayer("Alice");
            PlayerHand playerHand = Api.RegisterPlayerHand("ah,2h,3h,4h,5h");
            alice.PlayerHand = playerHand;
            players.Add(alice);

            Assert.Throws(typeof(InvalidPokerRuleException), delegate {
                Api.GetWinners(players);
            });
        }

        [Test()]
        public void Test_RegisterRules_Success()
        {
            Api.RegisterRules(HandRank.HighCard);

            List<Player> players = new List<Player>();
            Player alice = Api.RegisterPlayer("Alice");
            Player bob = Api.RegisterPlayer("Bob");
            PlayerHand playerHand = Api.RegisterPlayerHand("ah,2h,3h,4h,5h");
            PlayerHand playerHand2 = Api.RegisterPlayerHand("Kh,Ks,5c,6d,7s");
            alice.PlayerHand = playerHand;
            bob.PlayerHand = playerHand2;
            players.Add(alice);
            players.Add(bob);

            List<Player> winner = Api.GetWinners(players);
            Assert.IsTrue(winner.Count == 1);
            Assert.AreEqual(winner[0].Name, "Alice");

            bob.PlayerHand = playerHand;
            winner = Api.GetWinners(players);
            Assert.IsTrue(winner.Count == 2);
        }

        [Test()]
        public void Test_GetWinners_ThrowException()
        {
            Api.RegisterRules(HandRank.HighCard);
            List<List<Player>> playerLists = new List<List<Player>>();
            List<Player> players = new List<Player>
            {
                new Player("Alice")
            };
            playerLists.Add(null);
            playerLists.Add(new List<Player>());
            playerLists.Add(players);
            Assert.Throws(typeof(InvalidGameException), delegate
            {
                Api.GetWinners(playerLists[0]);
            });
            Assert.Throws(typeof(InvalidGameException), delegate
            {
                Api.GetWinners(playerLists[1]);
            });
            Assert.Throws(typeof(InvalidPlayerException), delegate
            {
                Api.GetWinners(playerLists[2]);
            });
        }
    }
}
