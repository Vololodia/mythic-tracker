using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameDeck
{
    public class DeckCard
    {
        public int? Position { get; set; }

        public DeckCard(int? position)
        {
            Position = position;
        }
    }
}
