using NUnit.Framework;
using System;
using Library.Exceptions;
using Library.Model;
using Library.Utilities;

namespace Test.Library.Utilities
{
    [TestFixture()]
    public class ParserTest
    {
        [Test()]
        public void Test_ParseCard_ThrowException()
        {
            string[] invalidCardString = {"10T", "23S", "1D",
            "tD"};
            foreach (string invalid in invalidCardString)
            {
                Assert.Throws(typeof(InvalidCardException), delegate
                {
                    Parser.ParseCard(invalid);
                });
            }
        }

        [Test()]
        public void Test_ParseCard_Success()
        {
            string[] validCardString = { "AH", "5H", "QD", "10S", "JH", "KC" };

            CardRank[] expectedRank = {CardRank.Ace, CardRank.Five,
                CardRank.Queen, CardRank.Ten, CardRank.Jack,CardRank.King};
            CardSuit[] expectedSuit = {CardSuit.HEART, CardSuit.HEART,
                CardSuit.DIAMOND, CardSuit.SPADE, CardSuit.HEART, CardSuit.CLUB};

            for (int i = 0; i < validCardString.Length; i++)
            {
                Assert.IsTrue(Parser.ParseCard(
                  validCardString[i]).Rank == expectedRank[i]);
                Assert.IsTrue(Parser.ParseCard(
                  validCardString[i]).Suit == expectedSuit[i]);
            }
        }
    }


}
