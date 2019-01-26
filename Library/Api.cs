using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Library.Model;
using Library.Utilities;
using Library.Exceptions;

namespace Library
{
    public static class Api
    {
        public static List<HandRank> rules = new List<HandRank>();
        private static readonly HandRank[] ALLRULES = {HandRank.HighCard,
            HandRank.OnePair, HandRank.ThreeOfAKind, HandRank.Flush};
        private static List<Card> usedCards = new List<Card>();
        private const int CARDNUMBER = 5;


        public static Player RegisterPlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidPlayerException("name");
            Player player = new Player(name);
            return player;
        }

        public static PlayerHand RegisterPlayerHand(string cardString)
        {
            if (string.IsNullOrWhiteSpace(cardString))
                throw new InvalidCardException("empty string is not allowed");

            string[] cards = cardString.Split(',');
            if (cards == null || cards.Length != CARDNUMBER)
                throw new InvalidCardException("incorrect number of cards");
            List<Card> validCards = new List<Card>();
            Regex rgx = new Regex(@"(A|J|Q|K|[2-9]|10)(H|C|D|S)");
            String formatCardString;
            foreach (string card in cards)
            {
                formatCardString = card.Trim().ToUpper();
                if (!rgx.IsMatch(formatCardString))
                    throw new InvalidCardException("card is not valid");

                Card validCard = Parser.ParseCard(formatCardString);

                if (usedCards.Any((usedCard) => 
                    usedCard.CompareTo(validCard)== 0
                    && usedCard.Suit == validCard.Suit))
                        throw new InvalidCardException("duplicate card");

                validCards.Add(validCard);
            }

            if (validCards.GroupBy((validCard) => 
             new { validCard.Rank, validCard.Suit })
            .Any((grp) => grp.Count() > 1))
                throw new InvalidCardException("duplicate card");

            usedCards.AddRange(validCards);
            return new PlayerHand(validCards);

        }

        public static void RegisterRules(HandRank ruleName)
        {
            if (!ALLRULES.Contains(ruleName))
                throw new InvalidPokerRuleException(ruleName.ToString());
            else if (!rules.Contains(ruleName))
                rules.Add(ruleName);
        }

        public static List<Player> GetWinners(List<Player> players)
        {
            if (players == null || players.Count == 0)
                throw new InvalidGameException("no player is added");
            if (players.Any(player => player.PlayerHand == null))
                throw new InvalidPlayerException("player hand");
            Analyser analyser = new Analyser(rules);
            players.ForEach((player) => {
                analyser.SetHandRank(player.PlayerHand);
                Console.WriteLine(player.Name + " rank is " + player.PlayerHand.HandRank);
            });
            
            Player highest = players.OrderByDescending((player) => player.PlayerHand).FirstOrDefault();
            List<Player> winners = players.Where((player) => player.PlayerHand.CompareTo(highest.PlayerHand)==0).ToList();
            return winners;
        }
    }
}
