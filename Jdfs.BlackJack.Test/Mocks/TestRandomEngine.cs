using Jdfs.BlackJack.Api.Business;
using Jdfs.BlackJack.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jdfs.BlackJack.Test.Mocks
{
    public class TestRandomEngine : IRandomEngine
    {
        private int mockIndex = 0;

        private List<Card> DealerCards { get; set; }
        private List<Card> GuestCards { get; set; }
        public List<Card> RandomCardsMock { get; set; }

        public TestRandomEngine(List<Card> dealerCards, List<Card> guestCards)
        {
            this.DealerCards = dealerCards;
            this.GuestCards = guestCards;
        }

        public TestRandomEngine(List<Card> dealerCards, List<Card> guestCards, List<Card> randomCardsMock) 
            : this(dealerCards, guestCards)
        {
            this.RandomCardsMock = randomCardsMock;
        }

        public IEnumerable<Card> GetDealerStartCards()
        {
            return DealerCards;
        }

        public IEnumerable<Card> GetGuestStartCards()
        {
            return GuestCards;
        }

        public IEnumerable<Card> GetRandomCards(int count)
        {
            if (mockIndex > RandomCardsMock.Count - 1)
                mockIndex = RandomCardsMock.Count - 1;

            var cards = RandomCardsMock
                .Skip(mockIndex)
                .Take(count);

            mockIndex += count;

            return cards;
        }
    }
}
