using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class ConnectionResponseMessage
    {
        [JsonProperty("status")]
        public ConnectionStatus Status { get; set; }

        [JsonProperty("deckMessage")]
        public DeckMessage DeckMessage { get; set; }
    }
}
