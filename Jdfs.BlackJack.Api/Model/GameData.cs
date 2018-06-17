using System;
using System.Collections.Generic;
using System.Linq;

namespace Jdfs.BlackJack.Api.Model
{
    [Serializable]
    public class GameData
    {
        public List<Card> DealerCards { get; set; }
        public List<Card> GuestCards { get; set; }
        public Player? Winner { get; set; }
        public bool GameOver { get; set; }
        public int CurrentDealerScore { get; set; }
        public int CurrentGuestScore { get; set; }
    }
}
