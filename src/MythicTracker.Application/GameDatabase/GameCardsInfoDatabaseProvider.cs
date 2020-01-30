using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson;


namespace MythicTracker.Application.GameDatabase
{
    public class GameCardsInfoDatabaseProvider : ICardDatabase
    {
        private string _pathToJsonDatabase;
        private Dictionary<int, Card> _idCardsSInfo;
        private Dictionary<int, AbilityCard> _idAbilitiesInfo;

        public GameCardsInfoDatabaseProvider(string pathToJsonDatabase)
        {
            this._pathToJsonDatabase = pathToJsonDatabase;
        }

        private void ConvertFromDatabaseToDictionary()
        {
            _idCardsSInfo = JsonConvert.DeserializeObject<Dictionary<int, Card>>(File.ReadAllText(_pathToJsonDatabase));

            _idAbilitiesInfo = JsonConvert.DeserializeObject<Dictionary<int, AbilityCard>>(File.ReadAllText(_pathToJsonDatabase));
        }

        Card ICardDatabase.GetCardOnID(int id)
        {
            Card card = _idCardsSInfo[id];
            if (card == null)
            {
                card = _idAbilitiesInfo[id];
            }

            return card;
        }
    }
}
