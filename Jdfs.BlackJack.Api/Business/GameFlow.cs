using Jdfs.BlackJack.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jdfs.BlackJack.Api.Business
{
    /// <summary>
    /// Controls the game
    /// </summary>
    public class GameFlow
    {
        /// <summary>
        /// Random engine for the game
        /// </summary>
        private IRandomEngine randomEngine;

        /// <summary>
        /// Application card list
        /// </summary>
        private List<Model.Card> cards;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="randomEngine">Random engine</param>
        public GameFlow(IRandomEngine randomEngine)
        {
            cards = ApplicationCards.GetCards();
            this.randomEngine = randomEngine;
        }

        /// <summary>
        /// Get score based on a list of cards
        /// </summary>
        /// <param name="cards">List of cards</param>
        /// <returns>Score value</returns>
        public int GetScore(List<Model.Card> cards)
        {
            // check whether it contains any card with a 10 rank value
            int tenCount = cards.Count(card => GetRankValue(card.Rank) == 10);

            List<Model.Card> aces = cards.Where(card => card.Rank == Rank.Ace).ToList();
            int acesSum = 0;
            if (aces.Count > 0)
            {
                acesSum = aces.Sum(card => GetRankValue(card.Rank));

                if (cards.Count == 2 && (tenCount == 1 || aces.Count == 2))
                {
                    // having a pair ace + ten, consider ace as 11 rank
                    acesSum += 10;
                }
            }

            // sum up
            return acesSum + cards
                .Where(card => card.Rank != Rank.Ace)
                .Sum(card => GetRankValue(card.Rank));
        }

        /// <summary>
        /// Game initializing
        /// </summary>
        /// <returns>Game data</returns>
        public GameData Initialize()
        {
            GameData data = new GameData()
            {
                DealerCards = randomEngine.GetDealerStartCards().ToList(),
                GuestCards = randomEngine.GetGuestStartCards().ToList()
            };

            // dealer's hidden card
            data.DealerCards.First().Hidden = true;

            // check black jack
            int guestScore = GetScore(data.GuestCards);
            int dealerScore = GetScore(data.DealerCards);
            if (guestScore == 21 || dealerScore == 21)
            {
                data.GameOver = true;
                if (guestScore != dealerScore)
                    data.Winner = guestScore > dealerScore ? Player.Guest : Player.Dealer;

                RevealDealerCard(data);
            }

            UpdateScoreValues(data);

            return data;
        }

        /// <summary>
        /// Perform a hit action by the guest
        /// </summary>
        /// <param name="data">Game data</param>
        public void Hit(GameData data)
        {
            if (data.GameOver)
                return;

            data.GuestCards.AddRange(randomEngine.GetRandomCards(1).ToList());

            if (GetScore(data.GuestCards) >= 21)
            {
                Stand(data);
            }
            else
            {
                UpdateScoreValues(data);
            }
        }

        /// <summary>
        /// Perform a "stand" stand action so turning to host
        /// - The guest player is ok with its current score
        /// </summary>
        /// <param name="data">Game data</param>
        public void Stand(GameData data)
        {
            if (data.GameOver)
                return;

            data.GameOver = true;
            RevealDealerCard(data);

            if (GetScore(data.GuestCards) < 21)
            {
                while (data.DealerCards.Count < 5 && GetScore(data.DealerCards) <= 17)
                {
                    data.DealerCards.AddRange(randomEngine.GetRandomCards(1).ToList());
                }
            }

            data.Winner = CheckWinner(GetScore(data.DealerCards), GetScore(data.GuestCards));

            UpdateScoreValues(data);
        }

        /// <summary>
        /// Returns a custom copy of the data for the clients view
        /// </summary>
        /// <param name="data">Source data to be parsed</param>
        /// <returns></returns>
        public GameData GetclientView(GameData data)
        {
            GameData client = new GameData
            {
                DealerCards = data.DealerCards.Select(card => card).ToList(),
                GuestCards = data.GuestCards.Select(card => card).ToList(),
                Winner = data.Winner,
                GameOver = data.GameOver,
                CurrentDealerScore = data.CurrentDealerScore,
                CurrentGuestScore = data.CurrentGuestScore
            };

            var hiddenCard = client.DealerCards.FirstOrDefault(card => card.Hidden);
            if (hiddenCard != null)
            {
                hiddenCard.Rank = Rank.Hidden;
                hiddenCard.Suit = Suit.Hidden;
            }

            return client;
        }

        /// <summary>
        /// Check whether there is a winner based on score values
        /// </summary>
        /// <param name="dealerScore">Dealer current score</param>
        /// <param name="guestScore">Guest current score</param>
        /// <returns>Winner (or null if draw)</returns>
        private Player? CheckWinner(int dealerScore, int guestScore)
        {
            if (dealerScore == guestScore)
                return null;

            if ((guestScore > dealerScore || dealerScore > 21) && guestScore <= 21)
                return Player.Guest;

            if ((dealerScore > guestScore || guestScore > 21) && dealerScore <= 21)
                return Player.Dealer;

            return null;
        }

        /// <summary>
        /// Face up the hidden card
        /// </summary>
        /// <param name="data">Game data</param>
        private void RevealDealerCard(GameData data)
        {
            var hiddenCard = data.DealerCards.FirstOrDefault(card => card.Hidden);
            if (hiddenCard != null)
                hiddenCard.Hidden = false;
        }

        /// <summary>
        /// Get rank value based on an enum item
        /// </summary>
        /// <param name="rank">Enum item</param>
        /// <returns>Rank value</returns>
        private int GetRankValue(Rank rank)
        {
            var memberInfo = typeof(Rank).GetField(Enum.GetName(typeof(Rank), rank));
            var rankValueAttr = ((RankValueAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(RankValueAttribute)));
            return rankValueAttr.Value;
        }

        /// <summary>
        /// Update score values to keep the client up to date
        /// </summary>
        /// <param name="data">Game data</param>
        private void UpdateScoreValues(GameData data)
        {
            data.CurrentDealerScore = GetScore(data.DealerCards);
            data.CurrentGuestScore = GetScore(data.GuestCards);

            var hiddenCard = data.DealerCards.FirstOrDefault(card => card.Hidden == true);
            if (hiddenCard != null)
                data.CurrentDealerScore -= GetRankValue(hiddenCard.Rank);
        }
    }
}
