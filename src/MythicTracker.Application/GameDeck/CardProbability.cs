using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameDeck
{
    public class CardProbability
    {
        private int Id { get; set; }

        public int TopDeckProbability { get; set; }

        public List<DeckCard> CardInstances { get; set; }

        public CardProbability(int id)
        {
            Id = id;
            TopDeckProbability = 0;
            CardInstances = new List<DeckCard>();
        }
    }
}
