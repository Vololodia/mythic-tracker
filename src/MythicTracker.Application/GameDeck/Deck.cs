using MythicTracker.Application.GameDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MythicTracker.Application.GameDeck
{
    public class Deck
    {
        private int[] _ids;
        private Dictionary<int, CardProbability> _deck;

        public Deck(int[] ids)
        {
            _ids = ids;
            Shuffle();
        }

        public void AddCard(int id)
        {
            _deck.Add(id, new CardProbability(id));
        }

        public void AddCards(int[] ids)
        {
            foreach (int id in ids)
            {
                _deck.Add(ids[id], new CardProbability(ids[id]));
            }
        }

        public void RemoveCard(int id)
        {
            _deck.Remove(id);
        }

        public void RemoveCards(int[] ids)
        {
            foreach (int id in ids)
            {
                _deck.Remove(ids[id]);
            }
        }

        public void Shuffle()
        {
            _deck = _ids.ToDictionary(x => x, y => new CardProbability(y));
        }

        public Deck ResetDeck(int[] id)
        {
            return new Deck(id);
        }
    }
}
