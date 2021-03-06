﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public class PokerLogic
    {
        private readonly Poker poker;
        private Dictionary<Cards, int> betToAdd;
        private readonly int HighPairBetAdded;
        private readonly int BettToAddBecauseOfPair;
        private readonly int BaseBet;

        private int alreadyBetted = 0;

        public string GameId { get; private set; }

        public PokerLogic(Poker poker)
        {
            this.poker = poker;
            GameId = poker.GameId;
            double factor = 0.6;
            HighPairBetAdded = (int)(100 * factor);
            BettToAddBecauseOfPair = (int)(500 * factor);
            BaseBet = (int) (100 * factor);
            betToAdd = new Dictionary<Cards, int>();
            betToAdd.Add(Cards.Ace, (int)(150 *factor));
            betToAdd.Add(Cards.King, (int)(100 * factor));
            betToAdd.Add(Cards.Queen, (int)(100 * factor));
            betToAdd.Add(Cards.Jack, (int)(100 * factor));
            betToAdd.Add(Cards.N10, (int)(60 * factor));
        }
       

        public int Play(JObject gameState)
        {
            int bet = BaseBet;
            List<Cards> cards = poker.GetOurCards();

            if (poker.CommunityMode)
            {
                bet = UseRankingServiceForBet();
            }
            else
            {
                bet = NoCommunityCardsSeen(bet);
            }


            return bet;
        }

        private int UseRankingServiceForBet()
        {
            int ranking = poker.RankId;
            if (ranking == 2 || ranking == 3)
            {
                return BaseBet*5;
            }

            if (ranking >= 4)
            {
                return 5000;
            }
            return BaseBet;
        }

        private int NoCommunityCardsSeen(int bet)
        {
            List<ICards> fullcards = poker.GetOurFullCards();
            List<ICards> cardsWeGet = fullcards.ToList();
            PrintCards(fullcards);


            int betToAdd = 0;
            bool doWeHavePair = BetBecauseOfPair(cardsWeGet, out betToAdd);
            if (doWeHavePair)
            {
                Console.WriteLine("We have pair");
                bet += betToAdd;
            }

            //bool doWeHaveSameColour = BetBecauseOfSameColour(cardsWeGet, out betToAdd);
            

            bet = BetBecauseOfHighCard(cardsWeGet, bet);

            int numberOfHighCards = GetNumberOffHighCards(cardsWeGet);

            Console.WriteLine("Number of high cards: " + numberOfHighCards);

            if (doWeHavePair && numberOfHighCards >= 2)
            {
                Console.WriteLine("We have a high pair");
                bet += HighPairBetAdded;
            }

            if (doWeHavePair && bet < poker.CurrentBuyIn)
            {
                bet = poker.CurrentBuyIn;
            }

            bet = DoBluffing(bet);

            alreadyBetted += bet;
            Console.WriteLine("Already betted: " + alreadyBetted);


            return bet;
        }

        private static void PrintCards(List<ICards> fullcards)
        {
            foreach (var card in fullcards)
            {
                Console.WriteLine("Card we get: " + card.Rank + "/" + card.Suit);
            }
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

        private bool BetBecauseOfPair(List<ICards> cardsWeGet, out int betToAdd)
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

        private bool BetBecauseOfSameColour(List<ICards> cardsWeGet, out int betToAdd)
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


        private int BetBecauseOfHighCard(List<ICards> cardsWeGet, int bet)
        {
            foreach (var card in cardsWeGet)
            {
                if (betToAdd.ContainsKey(card.Rank))
                {
                    Console.WriteLine("Found card" + card);
                    bet += betToAdd[card.Rank];
                    Console.WriteLine("Bet increased to" + bet);
                }
            }
            return bet;
        }

        private int GetNumberOffHighCards(List<ICards> cardsWeGet)
        {
            int numberOfHighCards = 0;

            foreach (var card in cardsWeGet)
            {
                if (betToAdd.ContainsKey(card.Rank))
                {
                    numberOfHighCards++;
                }
            }

            return numberOfHighCards;
        }

    }
}