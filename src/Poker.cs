using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    using Newtonsoft.Json.Linq;

    class Poker : IPoker
    {
        private JObject GameState;

        public Poker(JObject gameState)
        {
            GameState = gameState;
        }
        public List<Cards> GetOurCards()
        {
            return new List<Cards>();
        }
    }
}
