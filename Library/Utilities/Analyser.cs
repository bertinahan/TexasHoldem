using System;
using System.Collections.Generic;
using System.Linq;
using Library.Model;
using Library.Exceptions;

namespace Library.Utilities
{
    public class Analyser
    {
        private List<HandRank> Rules;

        /**
         * Analyser constructor
         */
        public Analyser()
            : this(new List<HandRank>())
        {
        }

        /** Analyser constructor
         * 
         * @param {List} rules user registed rules
         */
        public Analyser(List<HandRank> rules)
        {
            Rules = rules;
        }

        /**
         * Set player's playhand's rank and kickers
         * 
         * @param {PlayerHand} playerHand player's hand
         */
        public void SetHandRank(PlayerHand playerHand)
        {
            if (Rules == null || Rules.Count == 0)
                throw new InvalidPokerRuleException();
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

        /**
         * Determine if playhand is flush; if it is, remarks-order cards order
         * as kickers for tie, and update hand rank        
         * 
         * @param  {PlayerHand} playerHand player's hand
         * @return {Boolean}               if playerhand is flush
         */
        private bool IsFlush(PlayerHand playerHand)
        {
            if (playerHand.Cards.GroupBy((card) => card.Suit).Count() == 1)
            {
                playerHand.Cards = playerHand.Cards
                  .OrderByDescending((card) => card.Rank).ToList();
                playerHand.HandRank = HandRank.Flush;
                return true;
            }

            return  false;
        }

        /**
         * Determine if playhand is three of a kind; if it is, remarks-order
         * cards order as kickers for tie, and update hand rank        
         * 
         * @param  {PlayerHand} playerHand player's hand
         * @return {Boolean}               if playerhand is three of a kind
         */
        private bool IsThreeOfAKind(PlayerHand playerHand)
        {
            if (playerHand.Cards.GroupBy((card) => card.Rank)
                .Any(group => group.Count() > 2))
            {
                playerHand.Cards = playerHand.Cards
                                .OrderByDescending(c => c.Rank)
                .OrderByDescending(c => playerHand
                                .Cards.Count(c1 => c1.Rank == c.Rank))
                .ToList();
                playerHand.HandRank = HandRank.ThreeOfAKind;
                return true;
            }
            return false;
        }

        /**
         * Determine if playhand is one pair; if it is, remarks-order
         * cards order as kickers for tie, and update hand rank        
         * 
         * @param  {PlayerHand} playerHand player's hand
         * @return {Boolean}               if playerhand is one pair
         */
        private bool IsOnePair(PlayerHand playerHand)
        {
            if (playerHand.Cards.GroupBy((card) => card.Rank)
                .Any(group => group.Count() > 1))
            {
                playerHand.Cards = playerHand.Cards
                                .OrderByDescending(c => c.Rank)
                .OrderByDescending(c => playerHand
                                .Cards.Count(c1 => c1.Rank == c.Rank))
                .ToList();
                playerHand.HandRank = HandRank.OnePair;
                return true;
            }
            return false;
        }
    }
}
