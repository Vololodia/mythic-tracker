using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace MythicTracker.Application.GameDatabase
{
    public class FileCardsProvider : ICardDatabase
    {
        private readonly DatabaseModel _cardsInfo;

        public FileCardsProvider(string filepath)
        {
            _cardsInfo = JsonConvert.DeserializeObject<DatabaseModel>(File.ReadAllText(filepath));
        }

        public Card GetCard(int id)
        {
            return _cardsInfo.Cards.TryGetValue(id, out Card card) ? card : null;
        }

        public Card[] GetAllCards()
        {
            return _cardsInfo.Cards.Values.ToArray();
        }
    }
}
