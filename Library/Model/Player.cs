using System;
namespace Library.Model
{
    public class Player
    {
        public string Name { get; private set; }
        public PlayerHand PlayerHand { get; set; }
        public Player(string Name)
        {
            this.Name = Name;
            this.PlayerHand = null;
        }
    }
}
