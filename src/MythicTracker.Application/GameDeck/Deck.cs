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
            _idsList = new List<int>();
            _idsList.AddRange(_ids);
            Shuffle();
        }

        public void Shuffle()
        {
            _deck = _idsList.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => new CardProbability(x.Key) { CardInstances = x.Select(y => new DeckCard(null)).ToList() });
        }

        public void AddCardInDeck(int id, int position)
        {
            _idsList.Add(id);
            if (_deck.ContainsKey(id))
            {
                foreach (KeyValuePair<int, CardProbability> value in _deck)
                {
                    foreach (DeckCard card in value.Value.CardInstances)
                    {
                        if (card.Position != null)
                        {
                            if (card.Position >= position)
                            {
                                card.Position++;
                            }
                            else
                            {
                                card.Position--;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                _deck[id].CardInstances.Add(new DeckCard(position));
            }
            else
            {
                foreach (KeyValuePair<int, CardProbability> value in _deck)
                {
                    if (value.Value.CardInstances[0].Position != null)
                    {
                        if (value.Value.CardInstances[0].Position >= position)
                        {
                            value.Value.CardInstances[0].Position++;
                        }
                        else
                        {
                            value.Value.CardInstances[0].Position--;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                _deck.Add(id, new CardProbability(id));
                _deck[id].CardInstances.Add(new DeckCard(position));
            }
        }

        public Dictionary<int, CardProbability> GetDeck()
        {
            return _deck;
        }

        private void SortingDeck()
        {
            //foreach (KeyValuePair<int, CardProbability> value in _deck)
            //{
            //    foreach (DeckCard card in value.Value.CardInstances)
            //    {
            //        if (card.Position != null)
            //        {

            //        }
            //    }
            //}
        }

        public void RemoveCardInDeck(int id)
        {
            int copyCardsCounter = _deck[id].CardInstances.Count;
            var maxCardsPosition = _deck[id].CardInstances.Max(x => x.Position);
            var minCardsPosition = _deck[id].CardInstances.Min(x => x.Position);
            if (_deck.ContainsKey(id))
            {
                _idsList.Remove(id);
                _deck.Remove(id);
                foreach (KeyValuePair<int, CardProbability> value in _deck)
                {
                    foreach (DeckCard card in value.Value.CardInstances)
                    {
                        if (card.Position != null)
                        {
                            if (card.Position > maxCardsPosition )
                            {
                                card.Position = card.Position - copyCardsCounter;
                            }

                            if (card.Position > minCardsPosition && card.Position < maxCardsPosition)
                            {
                                card.Position--;
                            }
                        }
                    }
                }
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
