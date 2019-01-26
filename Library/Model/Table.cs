using System;
using System.Collections.Generic;

namespace Library.Model
{
    public class Table
    {
        private List<Player> players;
        private List<Card> usedCards;
        private static int initTableNumber = 1;
        private int tableId;
        public Table()
        {
            players = new List<Player>();
            tableId = initTableNumber;
            initTableNumber++;
        }
        public void AddPlayer(Player player)
        {
            if (!players.Contains(player))
                players.Add(player);
        }
        public void collectCards()
    }
}
