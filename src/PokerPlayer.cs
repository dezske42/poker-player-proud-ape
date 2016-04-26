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

	            Log("Test begin");
                dynamic stuff = JsonConvert.DeserializeObject(gameState.ToString());

                foreach (var player in stuff.players)
                {
                    Log(player.name);
                }

                Log("Test end");
            }
            catch (Exception ex)
            {
                Log("Exception " + ex.Message);
	            return 50;
            }
            
            return 50;
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

