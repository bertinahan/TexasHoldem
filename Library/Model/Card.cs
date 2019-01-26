using System;
using Library.Utilities;

namespace Library.Model
{
    public class Card : IComparable<Card>
    {
        public CardRank Rank { get; private set; }
        public CardSuit Suit { get; private set; }
        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public int CompareTo(Card otherCard)
        {
            return Rank - otherCard.Rank;
        }

        public override string ToString()
        {
            return Rank.ToString() + " " + Suit.ToString();
        }
    }
}
