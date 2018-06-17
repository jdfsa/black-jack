using System;

namespace Jdfs.BlackJack.Api.Model
{
    /// <summary>
    /// Response model to the client view
    /// </summary>
    [Serializable]
    public class GameResponse
    {
        /// <summary>
        /// Token which contains the game state data
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Data to be returned to the client
        /// </summary>
        public GameData Data { get; set; }
    }
}
