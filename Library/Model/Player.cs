using System;
namespace Library.Model
{
    public class Player
    {
        public string Name { get; }
        public PlayerHand PlayerHand { get; set; }

        /**
         * Player constructor
         * 
         */
        public Player()
            : this("")
        {
        }

        /**
         * Player constructor
         * 
         * @param {string} Name name of player
         */
        public Player(string Name)
        {
            this.Name = Name;
            this.PlayerHand = null;
        }
    }
}
