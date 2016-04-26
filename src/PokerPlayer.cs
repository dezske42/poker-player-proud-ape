using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
	{
		public static readonly string VERSION = "Default C# folding player";

		public static int BetRequest(JObject gameState)
		{
            try
            {
                Poker poker = new Poker(gameState);
                var cards = poker.GetOurCards();

                //return FirstVersion(gameState);
                return SecondVersion(gameState);
            }
            catch (Exception ex)
            {
                Log("Exception " + ex.Message);
	            return 50;
            }
            
            
		}

        private static int SecondVersion(JObject gameState)
        {
            int bet = 50;
            Poker poker = new Poker(gameState);
            List<Cards> cards = poker.GetOurCards();

            Dictionary<Cards, int> betToAdd = new Dictionary<Cards, int>();
            betToAdd.Add(Cards.Ace, 15);
            betToAdd.Add(Cards.King, 10);
            

            foreach (var card in cards)
            {
                if (betToAdd.ContainsKey(card))
                {
                    Console.WriteLine("Found card" + card);
                    bet += betToAdd[card];
                    Console.WriteLine("Bet increased to" + bet);
                }
            }

            return bet;
        }

        private static int FirstVersion(JObject gameState)
        {
            Log("Test begin");
            dynamic stuff = JsonConvert.DeserializeObject(gameState.ToString());

            foreach (var player in stuff.players)
            {
                Log(player.name);
            }

            Log("Test end");
            return 50;
        }


        public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

        public static void Log(object log)
        {
            Console.WriteLine("[ProudApes] " + log.ToString());
        }
	}
}

