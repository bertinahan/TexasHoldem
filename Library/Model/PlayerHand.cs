using System;
using System.Linq;
using System.Collections.Generic;
using Library.Utilities;

namespace Library.Model
{
    public class PlayerHand : IComparable<PlayerHand>
    {
      
        public List<Card> Cards { get; set; }
        public HandRank HandRank { get; set; }
        public PlayerHand()
            : this(new List<Card>())
        {
        }
        public PlayerHand(List<Card> cards)
        {
            Cards = cards;
        }

        public int CompareTo(PlayerHand otherHand)
        {
            if (otherHand == null || HandRank > otherHand.HandRank) return 1;
            else if (HandRank == otherHand.HandRank)
            {
                for(int i = 0; i < Cards.Count(); i ++)
                {
                    if (Cards[i].Rank != otherHand.Cards[i].Rank)
                        return Cards[i].CompareTo(otherHand.Cards[i]);
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
