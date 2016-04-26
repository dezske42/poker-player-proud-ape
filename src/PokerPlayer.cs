using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    using System;
    using System.Runtime.Remoting.Messaging;

    public static class PokerPlayer
	{
		public static readonly string VERSION = "Default C# folding player";

		public static int BetRequest(JObject gameState)
		{
            try
            {
                Console.WriteLine("Test logging");

                //foreach (JProperty property in gameState.Properties())
                //{
                //    if (property.Name == "players")
                //    {
                //        Console.WriteLine("Test logging");
                //    }
                //}
            }
            catch (Exception)
            {
                // fallback
                return 50;
            }

            return 50;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

