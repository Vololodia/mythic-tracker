using Newtonsoft.Json;
using System.IO;
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
            if (_idCardsInfo.Cards.ContainsKey(id))
            {
                Card card = _idCardsInfo.Cards[id];
                return card;
            }
            else
            {
                return null;
            }
        }

        public Card[] GetAllCards()
        {
            var temp = _idCardsInfo;
            Card[] totalCards = new Card[temp.Cards.Count];
            totalCards = temp.Cards.Values.ToArray();
            return totalCards;
        }
    }
}
