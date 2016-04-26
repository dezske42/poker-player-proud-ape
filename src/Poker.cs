using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Poker : IPoker
    {
        private JObject GameState;

        public Poker(JObject gameState)
        {
            GameState = gameState;
        }

        public List<Cards> GetOurCards()
        {
            dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

            foreach (var player in stuff.players)
            {
                PokerPlayer.Log(player.name);
            }

            return new List<Cards>();
        }
    }
}
