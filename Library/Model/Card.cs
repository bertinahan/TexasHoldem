using System;
using Library.Utilities;

namespace Library.Model
{
    public class Card : IComparable<Card>
    {
        public CardRank Rank { get; }
        public CardSuit Suit { get; }

        /**
         * Card constructor
         * 
         * @param {CardRank} rank rank of the card
         * @param {CardSuit} suit suit of the card
         */
        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        /**
         * Compare card with other card
         * 
         * @param  {Card}     otherCard card to compare
         * @return {Integer}            compare result
         */
        public int CompareTo(Card otherCard)
        {
            if (otherCard == null) return 1;

            return Rank.CompareTo(otherCard.Rank);
        }

        /**
         * Convert card to string
         * 
         * @return {string} card rank and suit string
         */
        public override string ToString()
        {
            return Rank.ToString() + " " + Suit.ToString();
        }
    }
}
