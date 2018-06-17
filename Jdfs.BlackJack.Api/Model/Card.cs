using System;

namespace Jdfs.BlackJack.Api.Model
{
    /// <summary>
    /// Card model
    /// </summary>
    [Serializable]
    public class Card
    {
        /// <summary>
        /// Category
        /// </summary>
        public Suit Suit { get; set; }

        /// <summary>
        /// Card value
        /// </summary>
        public Rank Rank { get; set; }

        /// <summary>
        /// Indicate the dealer's hidden card
        /// Defaults: false
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Card() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="rank"></param>
        public Card(Suit suit, Rank rank) : this(suit, rank, false) { }

        /// <summary>
        /// Constructor for all params
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="rank"></param>
        /// <param name="hidden"></param>
        public Card(Suit suit, Rank rank, bool hidden)
        {
            Suit = suit;
            Rank = rank;
            Hidden = hidden;
        }
    }
}
