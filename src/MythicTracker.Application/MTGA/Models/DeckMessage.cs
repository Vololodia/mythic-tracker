using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class DeckMessage
    {
        [JsonProperty("deckCards")]
        public int[] DeckCards { get; set; }

        [JsonProperty("sideboardCards")]
        public int[] SideboardCards { get; set; }
    }
}
