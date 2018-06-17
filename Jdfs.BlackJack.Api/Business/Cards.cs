using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jdfs.BlackJack.Api.Business
{
    /// <summary>
    /// Application cards
    /// </summary>
    public class ApplicationCards
    {
        /// <summary>
        /// Static card list for the application
        /// </summary>
        private static List<Model.Card> Cards;

        /// <summary>
        /// Get application card list
        /// </summary>
        /// <returns></returns>
        public static List<Model.Card> GetCards()
        {
            if (Cards == null)
            {
                Cards = new List<Model.Card>();

                foreach (Model.Rank rank in Enum.GetValues(typeof(Model.Rank)))
                {
                    // ignore hidden option
                    if (rank == Model.Rank.Hidden)
                        continue;

                    foreach (Model.Suit suit in Enum.GetValues(typeof(Model.Suit)))
                    {
                        /// ignore hidden option
                        if (suit == Model.Suit.Hidden)
                            continue;

                        Cards.Add(new Model.Card
                        {
                            Rank = rank,
                            Suit = suit
                        });
                    }
                }
            }

            return Cards;
        }
    }
}
