using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace MythicTracker.Application.GameDatabase
{
    public class GameCardsInfoDatabaseProvider : ICardDatabase
    {
        private readonly DatabaseModel _idCardsInfo;

        public GameCardsInfoDatabaseProvider(string pathToJsonDatabase)
        {
            _idCardsInfo = JsonConvert.DeserializeObject<DatabaseModel>(File.ReadAllText(pathToJsonDatabase));
        }

        public Card GetCard(int id)
        {
            return _idCardsInfo.Cards.TryGetValue(id, out Card card) ? card : null;
        }

        public Card[] GetAllCards()
        {
            Card[] totalCards = _idCardsInfo.Cards.Values.ToArray();
            return totalCards;
        }
    }
}
