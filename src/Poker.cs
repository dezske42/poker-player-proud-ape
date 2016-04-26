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
                        else if (rank == "J")
                        {
                            cards.Add(Cards.Jack);
                        }
                        else if (rank == "Q")
                        {
                            cards.Add(Cards.Queen);
                        }
                        else if (rank == "2")
                        {
                            cards.Add(Cards.N2);
                        }
                        else if (rank == "3")
                        {
                            cards.Add(Cards.N3);
                        }
                        else if (rank == "4")
                        {
                            cards.Add(Cards.N4);
                        }
                        else if (rank == "5")
                        {
                            cards.Add(Cards.N5);
                        }
                        else if (rank == "6")
                        {
                            cards.Add(Cards.N6);
                        }
                        else if (rank == "7")
                        {
                            cards.Add(Cards.N7);
                        }
                        else if (rank == "8")
                        {
                            cards.Add(Cards.N8);
                        }
                        else if (rank == "9")
                        {
                            cards.Add(Cards.N9);
                        }
                        else if (rank == "10")
                        {
                            cards.Add(Cards.N10);
                        }
                        else
                        {
                            cards.Add(Cards.Unknown);
                        }
                    }
                }
            }

            return cards;
        }
    }
}
