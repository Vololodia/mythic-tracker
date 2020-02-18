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
        private List<int> _idsList;

        public Deck(int[] ids)
        {
            _ids = ids;
            _idsList.AddRange(_ids);
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
                _deck.Add(id, new CardProbability(id));
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
                _deck.Remove(id);
            }
        }

        public void Shuffle()
        {
            _deck = _ids.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .ToDictionary(g => g.Key, y => new CardProbability(y.Key) {   });
            var idsGroups = _ids.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key);
            _deck = idsGroups.ToDictionary(x => x, y => new CardProbability(y));
        }

        public void AddCardInDeck(int id, int cardPosition)
        {
            _idsList.Insert(cardPosition, id);
            _deck[id].TopDeckProbability = 100;
            _deck[id].CardInstances.Add(new DeckCard());
        }

        public void AddCardsInDeck(int[] ids)
        {
            foreach (int id in ids)
            {
                _idsList.Add(id);
                _deck[id].TopDeckProbability = 100;
                _deck[id].CardInstances.Add(new DeckCard());
            }
        }

        public void CardReplacement(int firstIndex, int secondIndex)
        {
            var temp = _deck[secondIndex];
            _deck[secondIndex] = _deck[firstIndex];
            _deck[firstIndex] = temp;
        }

        public Deck ResetDeck(int[] id)
        {
            return new Deck(id);
        }
    }
}
