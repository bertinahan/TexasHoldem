using System;
using Library.Model;
using Library.Exceptions;

namespace Library.Utilities
{
    public static class Parser
    {
    
        public static Card ParseCard(string card)
        {
            int cardLength = card.Length;
            string rank = card.Substring(0, cardLength - 1);
            string suit = card.Substring(cardLength - 1);
            CardSuit cardSuit;
            CardRank cardRank;
            switch (suit)
            {
                case "C": cardSuit = CardSuit.CLUB; break;
                case "D": cardSuit = CardSuit.DIAMOND; break;
                case "H": cardSuit = CardSuit.HEART; break;
                case "S": cardSuit = CardSuit.SPADE; break;
                default:
                  throw new InvalidCardException("card suit does not exist");
            }
            switch(rank)
            {
                case "J": cardRank = CardRank.Jack; break;
                case "Q": cardRank = CardRank.Queen; break;
                case "K": cardRank = CardRank.King; break;
                case "A": cardRank = CardRank.Ace; break;
                default: 
                    int value = Convert.ToInt32(rank);
                    if (value < 2 || value > 10)
                        throw new InvalidCardException(
                        "card rank does not exist");
                    cardRank = (CardRank)value;
                    break;
            }
            return new Card(cardRank, cardSuit);
        }
    }
}
