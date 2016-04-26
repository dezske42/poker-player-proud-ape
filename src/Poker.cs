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
            List<Cards> cards = new List<Cards>();

            dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

            foreach (var player in stuff.players)
            {
                if (player.name.ToString() == "Proud Ape")
                {
                    foreach (var card in player.hole_cards)
                    {
                        string rank = card.rank.ToString();

                        PokerPlayer.Log("Card: " + rank);

                        if (rank == "A")
                        {
                            cards.Add(Cards.Ace);
                        }
                        else if (rank == "K")
                        {
                            cards.Add(Cards.King);
                        }
                    }
                }
            }

            return cards;
        }
    }
}
