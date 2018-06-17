using Jdfs.BlackJack.Api.Business;
using Jdfs.BlackJack.Api.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jdfs.BlackJack.Test
{
    [TestClass]
    public class GameFlowScoreTest
    {
        private GameFlow gameFlow;

        public GameFlowScoreTest()
        {
            gameFlow = new GameFlow(new GameRandomEngine());
        }

        /// <summary>
        /// Test for a simple score case
        /// </summary>
        [TestMethod]
        public void GetSimpleScore()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Clubs, Rank.Eight),
                new Card(Suit.Diamonds, Rank.Four),
                new Card(Suit.Harts, Rank.King)
            };

            Assert.AreEqual<int>(22, gameFlow.GetScore(cards), "Score should be 22");
        }

        /// <summary>
        /// Test the score given an ace and no 10 (jack, queen, king and 10 itself)
        /// </summary>
        [TestMethod]
        public void GetSimpleScoreAceWithNoJack()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Clubs, Rank.Ace),
                new Card(Suit.Diamonds, Rank.Deuce),
                new Card(Suit.Harts, Rank.Four),
                new Card(Suit.Spades, Rank.Nine)
            };

            Assert.AreEqual<int>(16, gameFlow.GetScore(cards), "Score should be 16");
        }

        /// <summary>
        /// Test the score given a pair of aces
        /// </summary>
        [TestMethod]
        public void GetScoreDoubleAces()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Diamonds, Rank.Ace),
                new Card(Suit.Spades, Rank.Ace)
            };

            Assert.AreEqual<int>(12, gameFlow.GetScore(cards), "Score should be 12");
        }

        /// <summary>
        /// Test the score given an ace and any 10 rank
        /// </summary>
        [TestMethod]
        public void GetScoreAceWithJack()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Diamonds, Rank.Ace),
                new Card(Suit.Spades, Rank.Jack)
            };

            Assert.AreEqual<int>(21, gameFlow.GetScore(cards), "Score should be 21");
        }

        /// <summary>
        /// Test the score given an ace and two 10 rank
        /// </summary>
        [TestMethod]
        public void GetScoreAceWithTwoTen()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Clubs, Rank.Ace),
                new Card(Suit.Diamonds, Rank.Queen),
                new Card(Suit.Spades, Rank.Ten)
            };

            Assert.AreEqual<int>(21, gameFlow.GetScore(cards), "Score should be 21");
        }

        /// <summary>
        /// Test the score given a double aces but with no 10
        /// </summary>
        [TestMethod]
        public void GetScoreDoubleAce()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Diamonds, Rank.Eight),
                new Card(Suit.Clubs, Rank.Ace),
                new Card(Suit.Spades, Rank.Ace)
            };

            Assert.AreEqual<int>(10, gameFlow.GetScore(cards), "Score should be 10");
        }

        /// <summary>
        /// Test the score given a double aces and any 10 rank
        /// </summary>
        [TestMethod]
        public void GetScoreDoubleAceAndJack()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Clubs, Rank.Eight),
                new Card(Suit.Diamonds, Rank.Ace),
                new Card(Suit.Harts, Rank.Ace),
                new Card(Suit.Spades, Rank.Jack)
            };

            Assert.AreEqual<int>(20, gameFlow.GetScore(cards), "Score should be 20");
        }

        /// <summary>
        /// Test the score given a double aces and double queens
        /// </summary>
        [TestMethod]
        public void GetScoreDoubleAceAndDoubleQueen()
        {
            List<Card> cards = new List<Card>()
            {
                new Card(Suit.Harts, Rank.Queen),
                new Card(Suit.Spades, Rank.Ace),
                new Card(Suit.Diamonds, Rank.Ace),
                new Card(Suit.Spades, Rank.Queen)
            };

            Assert.AreEqual<int>(22, gameFlow.GetScore(cards), "Score should be 22");
        }
    }
}
