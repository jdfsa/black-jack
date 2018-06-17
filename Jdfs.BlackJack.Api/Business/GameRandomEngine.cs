using Jdfs.BlackJack.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jdfs.BlackJack.Api.Business
{
    /// <summary>
    /// Random engine
    /// </summary>
    public class GameRandomEngine : IRandomEngine
    {
        Random random = new Random();

        /// <summary>
        /// Get the dealer's initial cards
        /// </summary>
        /// <returns>Card list</returns>
        public IEnumerable<Card> GetDealerStartCards()
        {
            return GetRandomCards(2);
        }

        /// <summary>
        /// Get the guest initial cards
        /// </summary>
        /// <returns>Card list</returns>
        public IEnumerable<Card> GetGuestStartCards()
        {
            return GetRandomCards(2);
        }

        /// <summary>
        /// Get random card(s)
        /// </summary>
        /// <param name="count">Define how much random cards will be returned</param>
        /// <returns>Card list</returns>
        public IEnumerable<Card> GetRandomCards(int count)
        {
            for (int i = 0; i < count; i++)
                yield return new Card(GetRandomSuit(), GetRandomRank());
        }

        /// <summary>
        /// Get a random Rank
        /// </summary>
        /// <returns>Rank</returns>
        private Rank GetRandomRank()
        {
            return Enum.GetValues(typeof(Rank)).Cast<Rank>().ToArray()[random.Next(0, 12)]; 
        }

        /// <summary>
        /// Get a random Suit
        /// </summary>
        /// <returns>Suit</returns>
        private Suit GetRandomSuit()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>().ToArray()[random.Next(0, 3)];
        }
    }
}
