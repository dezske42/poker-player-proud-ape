using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    using System;

    public static class PokerPlayer
	{
		public static readonly string VERSION = "Default C# folding player";

		public static int BetRequest(JObject gameState)
		{
		    //try
		    //{
      //          foreach (JProperty property in gameState.Properties())
      //          {
      //              if (property.Name == "players")
      //              {
      //                  property.Value[]
      //              }
      //          }           
      //      }
      //      catch (Exception)
		    //{
      //          // fallback
		    //    return 50;
		    //}

			return 50;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

