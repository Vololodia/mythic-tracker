using System;
using System.Collections.Generic;
using System.Text;
using MythicTracker.Application.GameDatabase;

namespace MythicTracker.Application.GameDeck
{
    public class Deck
    {
        private int[] _id;
        private FileCardsProvider _provider;
        private Dictionary<int, CardProbability> _deck;

        public Deck(int[] id)
        {
            this._id = id;
            _deck = new Dictionary<int, CardProbability>();
        }

        public void AddCard(int id)
        {
            _deck.Add(id, new CardProbability(id));
        }

        public void AddCardsSet(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                _deck.Add(id[i], new CardProbability(id[i]));
            }
        }

        public void RemoveCard(int id)
        {
            _deck.Remove(id);
        }

        public void RemoveCardsSet(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                _deck.Remove(id[i]);
            }
        }

        public Dictionary<int, CardProbability> Shuffle()
        {
            _deck = new Dictionary<int, CardProbability> { };
            return _deck;
        }

        public Deck ResetDeck(int[] id)
        {
            return new Deck(id);
        }
    }
}
