using System;
using System.Collections.Generic;
using System.Linq;
using Library.Model;

namespace Library.Utilities
{
    public class Analyser
    {
        private List<HandRank> Rules;
        public Analyser()
            : this(new List<HandRank>())
        {

        }

        public Analyser(List<HandRank> rules)
        {
            Rules = rules;
        }

        public void SetHandRank(PlayerHand playerHand)
        {
            HandRank rank = HandRank.HighCard;
            foreach (HandRank rule in Rules)
            {
                switch (rule)
                {
                    case HandRank.Flush:
                        if (rank < HandRank.Flush && IsFlush(playerHand))
                            rank = HandRank.Flush;
                        break;
                    case HandRank.ThreeOfAKind:
                        if (rank < HandRank.ThreeOfAKind && IsThreeOfAKind(playerHand))
                            rank = HandRank.ThreeOfAKind;
                        break;
                    case HandRank.OnePair:
                        if (rank < HandRank.OnePair && IsOnePair(playerHand))
                            rank = HandRank.OnePair;
                        break;
                    case HandRank.HighCard:
                        if (rank <= HandRank.HighCard)
                        {
                            playerHand.Cards = playerHand.Cards
                             .OrderByDescending((card) => card.Rank).ToList();
                            playerHand.HandRank = HandRank.HighCard;
                        }
                        break;
                }
            }
        }

        private bool IsFlush(PlayerHand playerHand)
        {
            if (playerHand.Cards.GroupBy((card) => card.Suit).Count() == 1)
            {
                playerHand.Cards = playerHand.Cards.OrderByDescending((card) => card.Rank).ToList();
                playerHand.HandRank = HandRank.Flush;
                return true;
            }

            return  false;
        }

        private bool IsThreeOfAKind(PlayerHand playerHand)
        {
            if (playerHand.Cards.GroupBy((card) => card.Rank).Any(group => group.Count() > 2))
            {
                playerHand.Cards = playerHand.Cards
                                .OrderByDescending(c => c.Rank)
                .OrderByDescending(c => playerHand.Cards.Where(c1 => c1.Rank == c.Rank).Count())
                .ToList();
                playerHand.HandRank = HandRank.ThreeOfAKind;
                return true;
            }
            return false;
        }

        private bool IsOnePair(PlayerHand playerHand)
        {
            if (playerHand.Cards.GroupBy((card) => card.Rank).Any(group => group.Count() > 1))
            {
                playerHand.Cards = playerHand.Cards
                                .OrderByDescending(c => c.Rank)
                .OrderByDescending(c => playerHand.Cards.Count(c1 => c1.Rank == c.Rank))
                .ToList();
                playerHand.HandRank = HandRank.OnePair;
                return true;
            }
            return false;
        }
    }
}
