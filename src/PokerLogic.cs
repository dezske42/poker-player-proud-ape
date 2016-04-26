using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public class PokerLogic
    {
        private Dictionary<Cards, int> betToAdd;
        private const int BettToAddBecauseOfPair = 300;
        private const int BaseBet = 100;
        

        public PokerLogic()
        {
        
            betToAdd = new Dictionary<Cards, int>();
            betToAdd.Add(Cards.Ace, 15);
            betToAdd.Add(Cards.King, 10);
            betToAdd.Add(Cards.Queen, 10);
            betToAdd.Add(Cards.Jack, 10);
        }



        public int Play(JObject gameState)
        {
            int bet = BaseBet;
            Poker poker = new Poker(gameState);
            List<Cards> cards = poker.GetOurCards();
           

            List<Cards> cardsWeGet = new List<Cards>();

            foreach (var card in cards)
            {
                cardsWeGet.Add(card);
                int betToAdd = 0; 
                if (BetBecauseOfPair(cardsWeGet, out betToAdd))
                {
                    bet += betToAdd;
                }
                else
                {
                    bet = BetBecauseOfHighCard(card, bet);
                }
            }


            return bet;
        }

        private static bool BetBecauseOfPair(List<Cards> cardsWeGet, out int betToAdd)
        {
            betToAdd = 0;
            var duplicateKeys = cardsWeGet.GroupBy(x => x)
                .Where(group => @group.Count() > 1)
                .Select(group => @group.Key);

            foreach (var pair in duplicateKeys)
            {
                Console.WriteLine("Found pair: " + pair);
                betToAdd += BettToAddBecauseOfPair;
            }

            return false;
        }

        private int BetBecauseOfHighCard(Cards card, int bet)
        {

           
            if (betToAdd.ContainsKey(card))
            {
                Console.WriteLine("Found card" + card);
                bet += betToAdd[card];
                Console.WriteLine("Bet increased to" + bet);
            }
            return bet;
        }
    }
}