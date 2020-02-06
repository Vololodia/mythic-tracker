using System;
using System.Collections.Generic;
using System.Text;
using MythicTracker.Application.GameDatabase;

namespace MythicTracker.Application.GameDeckState
{
    public class PlayerDeck
    {
        private int[] _cardId;
        private FileCardsProvider _provider;
        private List<Card> _deck;

        public PlayerDeck(int[] cardId)
        {
            this._cardId = cardId;
            _deck = new List<Card>();
        }

        public void AddCardInDeck(int id)
        {
            _deck.Add(_provider.GetCard(id));
        }

        public void AddSetCardsInDeck(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                _deck.Add(_provider.GetCard(id[i]));
            }
        }

        public void RemoveCardInDeck(int id)
        {
            _deck.RemoveAt(id);
        }

        public void RemoveSetCardsInDeck(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
            {
                _deck.RemoveAt(id[i]);
            }
        }

        public PlayerDeck CreateNewDeck(int[] id)
        {
            return new PlayerDeck(id);
        }
    }
}
