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

            for (int i = 0; i < ids.Length; i++)
            {
                _deck.Add(ids[i], new CardProbability(ids[i]));
            }
        }

        public void RemoveCard(int id)
        {
            _deck.Remove(id);
        }

        public void RemoveCards(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                _deck.Remove(ids[i]);
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
