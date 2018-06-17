using System;

namespace Jdfs.BlackJack.Api.Model
{
    /// <summary>
    /// Attribute to represent a rank in a decimal way
    /// </summary>
    public class RankValueAttribute : Attribute
    {
        /// <summary>
        /// Rank value
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Rank value</param>
        internal RankValueAttribute(int value)
        {
            this.Value = value;
        }
    }
}
