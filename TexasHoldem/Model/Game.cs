using System;
using System.Collections.Generic;
using Library;
using Library.Model;
using Library.Utilities;

namespace TexasHoldem.Model
{
    public class Game
    {
        private List<Player> players;

        public Game()
        {
            players = new List<Player>();
            Init();
        }

        public void Init()
        {
            try
            {
                Api.RegisterRules(HandRank.Flush);
                Api.RegisterRules(HandRank.ThreeOfAKind);
                Api.RegisterRules(HandRank.OnePair);
                Api.RegisterRules(HandRank.HighCard);
            } catch (Exception e)
            {
                DisplayInstruction(e.Message);
                Environment.Exit(0);
            }

        }

        public void StartGame()
        {

            string name;
            string cardString;
            Player player;

            do
            {
                DisplayInstruction(PromptForPlayerName());
                name = ReadPlayerInput();
                if (String.IsNullOrEmpty(name))
                {
                    DisplayWinners();
                    continue;
                }

                try
                {
                    player = Api.RegisterPlayer(name);
                    players.Add(player);
                }
                catch (Exception e)
                {
                    DisplayInstruction(e.Message);
                    continue;
                }

                while (true)
                {
                    DisplayInstruction(PromptForCardsString());
                    cardString = ReadPlayerInput();
                    try
                    {
                        PlayerHand playerHand = Api.RegisterPlayerHand(cardString);
                        player.PlayerHand = playerHand;
                        break;
                    }
                    catch (Exception e)
                    {
                        DisplayInstruction(e.Message);
                    }
                }
            } while (!String.IsNullOrEmpty(name));

        }
        public string ReadPlayerInput()
        {
            string name = Console.ReadLine();
            return name;
        }
        public void DisplayInstruction(String message)
        {
            Console.WriteLine(message);
        }
        public string PromptForPlayerName()
        {
            return "/--------------------------------------------------------/"
                + "\nPlease enter player name to add player, eg: Joe; or press"
                + "\n enter key to display winner and quit\n"
                + "/--------------------------------------------------------/"
                + "\n\n";

        }

        public string PromptForCardsString()
        {
            return "/--------------------------------------------------------/"
                + "\nPlease enter cards in a line and seperate by comma, "
                + "\neg:3H, 4H, 5H, 6H, 8H \n"
                + "/--------------------------------------------------------/"
                + "\n\n";
        }

        public void DisplayWinners()
        {
            try
            {
                List<Player> winners = Api.GetWinners(players);

                winners.ForEach((winner) => Console.WriteLine(winner.Name + " wins! "
                + " " + winner.PlayerHand.HandRank));
            } catch (Exception e)
            {
                DisplayInstruction(e.Message);
            }

        }

    }
}
