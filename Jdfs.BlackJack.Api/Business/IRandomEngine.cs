using Jdfs.BlackJack.Api.Model;
using System.Collections.Generic;

namespace Jdfs.BlackJack.Api.Business
{
    /// <summary>
    /// Interface for random engine aiming for better test results
    /// </summary>
    public interface IRandomEngine
    {
        /// <summary>
        /// Get the dealer's initial cards
        /// </summary>
        /// <returns></returns>
        IEnumerable<Card> GetDealerStartCards();

        /// <summary>
        /// Get the guest initial cards
        /// </summary>
        /// <returns></returns>
        IEnumerable<Card> GetGuestStartCards();

        /// <summary>
        /// Get random card(s)
        /// </summary>
        /// <param name="count">Define how much random cards will be returned</param>
        /// <returns></returns>
        IEnumerable<Card> GetRandomCards(int count);
    }
}
