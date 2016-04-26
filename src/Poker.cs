using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Nancy.Simple
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Poker : IPoker
    {
        private Dictionary<string, Cards> cardMap = new Dictionary<string, Cards>()
                                                        {
                                                            { "A", Cards.Ace },
                                                            { "K", Cards.King },
                                                            { "J", Cards.Jack },
                                                            { "Q", Cards.Queen },
                                                            { "2", Cards.N2 },
                                                            { "3", Cards.N3 },
                                                            { "4", Cards.N4 },
                                                            { "5", Cards.N5 },
                                                            { "6", Cards.N6 },
                                                            { "7", Cards.N7 },
                                                            { "8", Cards.N8 },
                                                            { "9", Cards.N9 },
                                                            { "10", Cards.N10 },
                                                        };

        private Dictionary<string, Suits> suitMap = new Dictionary<string, Suits>()
                                                        {
                                                            { "hearts", Suits.Hearts },
                                                            { "spades", Suits.Spades },
                                                            { "clubs", Suits.Clubs },
                                                            { "diamonds", Suits.Diamonds },
                                                        };

        private readonly JObject GameState;

        public Poker(JObject gameState)
        {
            GameState = gameState;
        }

        public List<Cards> GetOurCards()
        {
            List<Cards> cards = new List<Cards>();

            dynamic player = GetOurPlayer();

            if (player != null)
            {
                foreach (var card in player.hole_cards)
                {
                    string rank = card.rank.ToString();

                    cards.Add(cardMap.ContainsKey(rank) ? cardMap[rank] : Cards.Unknown);
                }
            }

            return cards;
        }

        public List<ICards> GetOurFullCards()
        {
            List<ICards> cards = new List<ICards>();

            dynamic player = GetOurPlayer();

            if (player != null)
            {
                cards = CollectCards(player.hole_cards);
            }

            return cards;
        }

        public int CurrentBuyIn
        {
            get
            {
                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                int buyIn;
                int.TryParse(stuff.current_buy_in.ToString(), out buyIn);            
                return buyIn;
            }
        }

        public int Pot
        {
            get
            {
                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                int pot;
                int.TryParse(stuff.pot.ToString(), out pot);
                return pot;
            }
        }

        public int MinimumRaise
        {
            get
            {
                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                int minimumRaise;
                int.TryParse(stuff.minimum_raise.ToString(), out minimumRaise);
                return minimumRaise;
            }
        }

        public int HighestStackForActiveOtherPlayers
        {
            get
            {
                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                int maxStack = 0;

                foreach (var player in stuff.players)
                {
                    if (player.name.ToString() == "Proud Ape")
                        continue;

                    if (player.status.ToString() != "active")
                        continue;

                    int stack;
                    if (int.TryParse(player.stack.ToString(), out stack))
                    {
                        if (stack > maxStack)
                        {
                            maxStack = stack;
                        }
                    }
                }

                return maxStack;
            }
        }

        public IList<ICards> CommunityCards
        {
            get
            {
                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                return CollectCards(stuff.community_cards);
            }
        }

        public IList<ICards> GetOurCardsWithCommunityCards
        {
            get
            {
                List<ICards> allCards = new List<ICards>();

                allCards.AddRange(GetOurFullCards());
                allCards.AddRange(CommunityCards);

                return allCards;
            }
        }

        public bool CommunityMode
        {
            get
            {
                return CommunityCards.Count != 0;
            }
        }

        public string GameId
        {
            get
            {
                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                return stuff.game_id.ToString();
            }
        }

        public string AllCardsJSon
        {
            get
            {
                List<string> allCards = new List<string>();

                dynamic player = GetOurPlayer();

                if (player != null)
                {
                    foreach (dynamic card in player.hole_cards)
                    {
                        allCards.Add(card.ToString());
                    }
                }

                dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

                foreach (dynamic card in stuff.community_cards)
                {
                    allCards.Add(card.ToString());
                }

                return string.Format("[ {0} ]", string.Join(",", allCards));
            }
        }

        public int RankId
        {
            get
            {
                var rankingObj = GetRanking();

                dynamic stuff = JsonConvert.DeserializeObject(rankingObj.ToString());

                int rank;
                int.TryParse(stuff.rank.ToString(), out rank);
                return rank;
            }
        }

        private IList<ICards> CollectCards(dynamic cardList)
        {
            List<ICards> cards = new List<ICards>();

            foreach (var card in cardList)
            {
                string rank = card.rank.ToString();
                string suit = card.suit.ToString();

                FullCard fullCard = new FullCard
                                        {
                                            Rank = cardMap.ContainsKey(rank) ? cardMap[rank] : Cards.Unknown,
                                            Suit = suitMap.ContainsKey(suit) ? suitMap[suit] : Suits.Unknown
                                        };
                
                cards.Add(fullCard);
            }

            return cards;
        }

        public dynamic GetOurPlayer()
        {
            dynamic stuff = JsonConvert.DeserializeObject(GameState.ToString());

            foreach (var player in stuff.players)
            {
                if (player.name.ToString() == "Proud Ape")
                {
                    return player;
                }
            }

            return null;
        }

        public JObject GetRanking()
        {
            System.Net.WebRequest request;
            request = WebRequest.Create("http://rainman.leanpoker.org/rank?cards=" + AllCardsJSon);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader resStreamReader = new StreamReader(resStream);
            String streamString = resStreamReader.ReadToEnd();
            Console.WriteLine(streamString);
            JObject rankingObj = JObject.Parse(streamString);
            return rankingObj;
        }
    }
}
