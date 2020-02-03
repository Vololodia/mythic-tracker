using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MythicTracker.Application.GameDatabase
{
    public class GameCardsInfoDatabaseProvider : ICardDatabase
    {
        private string _pathToJsonDatabase;
        private Root _idCardsInfo;

        public GameCardsInfoDatabaseProvider(string pathToJsonDatabase)
        {
            this._pathToJsonDatabase = pathToJsonDatabase;
            ConvertFromDatabaseToDictionary();
        }

        private void ConvertFromDatabaseToDictionary()
        {
            _idCardsInfo = JsonConvert.DeserializeObject<Root>(File.ReadAllText(_pathToJsonDatabase));
        }

        public Card GetCard(int id)
        {
            Card card = _idCardsInfo.Cards[id];
            //if (card == null)
            //{
            //    card = _idCardsInfo.Abilities[id];
            //}

            return card;
        }

        public Card[] GetAllCards()
        {
            var temp = _idCardsInfo;
            Card[] totalCards = new Card[temp.Cards.Count];
            temp.Cards.Values.CopyTo(totalCards, 0);
            return totalCards;
        }
    }
}
