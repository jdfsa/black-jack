using Jdfs.BlackJack.Api.Business;
using Jdfs.BlackJack.Api.Model;
using Jdfs.BlackJack.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jdfs.BlackJack.Test
{
    [TestClass]
    public class GameFlowTest
    {
        /*
        * INDEX PER RANK ACCORDINGLY TO THE ENUMERATOR
        * - [ 0] Ace = 1
        * - [ 1] Deuce = 2
        * - [ 2] Three = 3
        * - [ 3] Four = 4
        * - [ 4] Five = 5
        * - [ 5] Six = 6
        * - [ 6] Seven = 7
        * - [ 7] Eight = 8
        * - [ 8] Nine = 9
        * - [ 9] Ten = 10
        * - [10] Jack = 10
        * - [11] Queen = 10
        * - [12] King = 10
        */

        private Random random = new Random();

        /// <summary>
        /// Simples new game with no winners
        /// </summary>
        [TestMethod]
        public void NewGameNoWinner()
        {
            var dealerCards = GenerateCards(new int[] { 1, 4 }).ToList();
            var guestCards = GenerateCards(new int[] { 1, 6 }).ToList();
            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards));
            var data = game.Initialize();

            Assert.IsNotNull(game, "Should not be null");
            Assert.AreEqual<int>(2, data.DealerCards.Count, "Dealer should start with 2 cards");
            Assert.AreEqual<int>(1, data.DealerCards.Count(card => card.Hidden), "Dealer should start exacly with 1 hidden card");
            Assert.AreEqual<int>(2, data.GuestCards.Count, "Guest should start with 2 cards");
            Assert.IsTrue(data.GuestCards.All(card => !card.Hidden), "Guest should start with all its cards faced up");
            Assert.IsNull(data.Winner, "There should be no winner at the begining");
            Assert.AreEqual<int>(5, data.CurrentDealerScore, "Dealer score should be 5, wich is the only one visible card");
            Assert.AreEqual<int>(9, data.CurrentGuestScore, "Guest score should be 9");
        }

        public void  NewGameBlackJackDealer()
        {
            var dealerCards = GenerateCards(new int[] { 0, 12 }).ToList();
            var guestCards = GenerateCards(new int[] { 1, 9 }).ToList();
            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards));
            var data = game.Initialize();

            Assert.IsNotNull(data.Winner, "There should be a winner");
            Assert.AreEqual<Player?>(Player.Dealer, data.Winner.Value, "The winner should be the Dealer");
            Assert.IsTrue(data.DealerCards.All(card => !card.Hidden), "Dealer must face up its hidden card");
            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.AreEqual<int>(21, data.CurrentDealerScore, "Dealer score should be 21 considering that all cards are visible");
            Assert.AreEqual<int>(20, data.CurrentGuestScore, "Guest score should be 20");
        }

        [TestMethod]
        public void NewGameBlackJackGuest()
        {
            var dealerCards = GenerateCards(new int[] { 1, 5 }).ToList();
            var guestCards = GenerateCards(new int[] { 0, 11 }).ToList();
            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards));
            var data = game.Initialize();

            Assert.IsNotNull(data.Winner, "There should be a winner");
            Assert.AreEqual<Player?>(Player.Guest, data.Winner.Value, "The winner should be the Guest");
            Assert.IsTrue(data.DealerCards.All(card => !card.Hidden), "Dealer must face up its hidden card");
            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.AreEqual<int>(8, data.CurrentDealerScore, "Dealer score should be 8 considering that the game is over");
            Assert.AreEqual<int>(21, data.CurrentGuestScore, "Guest score should be 21");
        }

        [TestMethod]
        public void NewGameBlackJacDraw()
        {
            var dealerCards = GenerateCards(new int[] { 0, 9 }).ToList();
            var guestCards = GenerateCards(new int[] { 0, 11 }).ToList();
            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards));
            var data = game.Initialize();

            Assert.IsNull(data.Winner, "There should not be a winner");
            Assert.IsTrue(data.DealerCards.All(card => !card.Hidden), "Dealer must face up its hidden card");
            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.AreEqual<int>(21, data.CurrentDealerScore, "Dealer score should be 21 considering that the game is over");
            Assert.AreEqual<int>(21, data.CurrentGuestScore, "Guest score should be 21");
        }

        [TestMethod]
        public void GuestHitLooses()
        {
            var dealerCards = GenerateCards(new int[] { 4, 6 }).ToList();
            var guestCards = GenerateCards(new int[] { 3, 5 }).ToList();

            var randomIndexes = new int[] { 6, 10 };
            var mockCards = GenerateCards(randomIndexes).ToList();
            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards, mockCards));
            var data = game.Initialize();

            foreach (var item in randomIndexes)
                game.Hit(data);

            Assert.IsNotNull(data.Winner, "There should be a winner");
            Assert.AreEqual<Player?>(Player.Dealer, data.Winner.Value, "The winner should be the Guest");
            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.AreEqual<int>(12, data.CurrentDealerScore, "Dealer score should be 12 considering that the game is over");
            Assert.AreEqual<int>(27, data.CurrentGuestScore, "Guest score should be 27");
        }

        [TestMethod]
        public void GuestHitWins()
        {
            var dealerCards = GenerateCards(new int[] { 4, 6 }).ToList();
            var guestCards = GenerateCards(new int[] { 3, 5 }).ToList();

            var randomIndexes = new int[] { 0, 0, 8 };
            var mockCards = GenerateCards(randomIndexes).ToList();
            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards, mockCards));
            var data = game.Initialize();

            foreach (var item in randomIndexes)
                game.Hit(data);

            Assert.IsNotNull(data.Winner, "There should be a winner");
            Assert.AreEqual<Player?>(Player.Guest, data.Winner.Value, "The winner should be the Guest");
            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.AreEqual<int>(12, data.CurrentDealerScore, "Dealer score should be 12 considering that the game is over");
            Assert.AreEqual<int>(21, data.CurrentGuestScore, "Guest score should be 21");
        }

        [TestMethod]
        public void DealerWinsAfterStand()
        {
            var dealerCards = GenerateCards(new int[] { 3, 5 }).ToList();
            var guestCards = GenerateCards(new int[] { 4, 6 }).ToList();

            var randomIndexes = new int[] { 0, 0, 5 };
            var mockCards = GenerateCards(randomIndexes).ToList();

            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards, mockCards));
            var data = game.Initialize();
            game.Stand(data);

            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.IsTrue(data.DealerCards.Count <= 5, "Dealer should not have more than 5 cards");
            Assert.AreEqual<Player?>(Player.Dealer, data.Winner.Value, "The winner should be the Dealer");
            Assert.AreEqual<int>(18, data.CurrentDealerScore, "Dealer score should be 18 considering that the game is over");
            Assert.AreEqual<int>(12, data.CurrentGuestScore, "Guest score should be 12");
        }

        [TestMethod]
        public void DealerLosesAfterStandAbove21()
        {
            var dealerCards = GenerateCards(new int[] { 3, 5 }).ToList();
            var guestCards = GenerateCards(new int[] { 4, 6 }).ToList();

            var randomIndexes = new int[] { 4, 11 };
            var mockCards = GenerateCards(randomIndexes).ToList();

            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards, mockCards));
            var data = game.Initialize();
            game.Stand(data);

            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.IsTrue(data.DealerCards.Count <= 5, "Dealer should not have more than 5 cards");
            Assert.AreEqual<Player?>(Player.Guest, data.Winner.Value, "The winner should be the Guest");
            Assert.AreEqual<int>(25, data.CurrentDealerScore, "Dealer score should be 25 considering that the game is over");
            Assert.AreEqual<int>(12, data.CurrentGuestScore, "Guest score should be 12");
        }

        [TestMethod]
        public void DealerLosesAfterStandBelow21()
        {
            var dealerCards = GenerateCards(new int[] { 3, 5 }).ToList();
            var guestCards = GenerateCards(new int[] { 12, 9 }).ToList();

            var randomIndexes = new int[] { 0, 0, 0 };
            var mockCards = GenerateCards(randomIndexes).ToList();

            var game = new GameFlow(new TestRandomEngine(dealerCards, guestCards, mockCards));
            var data = game.Initialize();
            game.Stand(data);

            Assert.IsTrue(data.GameOver, "The game is over");
            Assert.IsTrue(data.DealerCards.Count <= 5, "Dealer should not have more than 5 cards");
            Assert.AreEqual<Player?>(Player.Guest, data.Winner.Value, "The winner should be the Guest");
            Assert.AreEqual<int>(13, data.CurrentDealerScore, "Dealer score should be 13 considering that the game is over");
            Assert.AreEqual<int>(20, data.CurrentGuestScore, "Guest score should be 20");
        }

        private IEnumerable<Card> GenerateCards(int[] indexes)
        {
            var rankValues = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();
            foreach (var index in indexes)
                yield return new Card(GetRandomSuit(), rankValues[index]);
        }

        private Suit GetRandomSuit()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>().ToArray()[random.Next(0, 3)];
        }
    }
}
