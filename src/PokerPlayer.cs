using System;
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

	            Console.WriteLine("test beginn");
                dynamic stuff = JsonConvert.DeserializeObject(gameState.ToString());

	            Console.WriteLine("Test: " + stuff.tournament_id);
            }
            catch (Exception)
            {
	            return 50;
            }
            
            return 1;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

        private static void Log(string log)
        {
            Console.WriteLine("[ProudApes] {0}", log);
        }
	}
}

