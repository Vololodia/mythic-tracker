using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameDeck
{
    public class CardProbability
    {
        private int Id { get; set; }

        private int TopDeckProbability { get; set; }

        private List<DeckCard> CardInstances { get; set; }

        public CardProbability(int id)
        {
            Id = id;
            TopDeckProbability = 0;
            CardInstances = new List<DeckCard>();
        }
    }
}
