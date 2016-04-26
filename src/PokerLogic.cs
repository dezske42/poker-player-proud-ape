﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public class PokerLogic
    {
        private Dictionary<Cards, int> betToAdd;
        private const int BettToAddBecauseOfPair = 500;
        private const int BaseBet = 100;
        

        public PokerLogic()
        {
        
            betToAdd = new Dictionary<Cards, int>();
            betToAdd.Add(Cards.Ace, 150);
            betToAdd.Add(Cards.King, 100);
            betToAdd.Add(Cards.Queen, 100);
            betToAdd.Add(Cards.Jack, 100);
            betToAdd.Add(Cards.N10, 60);
        }
       

        public int Play(JObject gameState)
        {
            int bet = BaseBet;
            Poker poker = new Poker(gameState);
            List<Cards> cards = poker.GetOurCards();
            List<ICards> fullcards = poker.GetOurFullCards();
           

            List<ICards> cardsWeGet = new List<ICards>();

            foreach (var card in fullcards)
            {
                Console.WriteLine("Card we get: "+ card.Rank + "/" + card.Suit);
                cardsWeGet.Add(card);
                int betToAdd = 0;
                bool doWeHavePair = BetBecauseOfPair(cardsWeGet, out betToAdd);
                if (doWeHavePair)
                {
                    Console.WriteLine("");
                    bet += betToAdd;
                }

                bool doWeHaveSameColour = BetBecauseOfSameColour(cardsWeGet, out betToAdd);
                if (doWeHaveSameColour)
                {
                    bet += betToAdd;
                }

                bet = BetBecauseOfHighCard(card.Rank, bet);

                if ((doWeHavePair || doWeHaveSameColour )&&
                    bet < poker.CurrentBuyIn)
                {
                    bet = poker.CurrentBuyIn;
                }

                bet = DoBluffing(bet);
            }


            return bet;
        }

        private int DoBluffing(int bet)
        {
            Random rnd = new Random();
            int number = rnd.Next(0, 100);

            int betToReturn = bet;
            if (number < 15)
            {
                double calcBet = bet*1.15;
                Console.WriteLine("Calculated bet" + calcBet);

                betToReturn = (int) calcBet;
            }

            return betToReturn;
        }

        private static bool BetBecauseOfPair(List<ICards> cardsWeGet, out int betToAdd)
        {
            bool found = false;
            betToAdd = 0;
            var duplicateKeys = cardsWeGet.Select(card => card.Rank)
                .GroupBy(x => x)
                .Where(group => @group.Count() > 1)
                .Select(group => @group.Key);

            foreach (var pair in duplicateKeys)
            {
                Console.WriteLine("Found pair: " + pair);
                betToAdd += BettToAddBecauseOfPair;
                found = true;
            }

            return found;
        }

        private static bool BetBecauseOfSameColour(List<ICards> cardsWeGet, out int betToAdd)
        {
            bool found = false;
            betToAdd = 0;
            var duplicateKeys = cardsWeGet.Select(c => c.Suit)
                .GroupBy(x => x)
                .Where(group => @group.Count() > 1)
                .Select(group => @group.Key);

            foreach (var pair in duplicateKeys)
            {
                Console.WriteLine("Found colour: " + pair);
                betToAdd += BettToAddBecauseOfPair;
                found = true;
            }

            return found;
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