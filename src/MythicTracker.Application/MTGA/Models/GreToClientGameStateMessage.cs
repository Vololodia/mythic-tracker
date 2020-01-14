using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GreToClientGameStateMessage : IGreToClientMessage
    {
        [JsonProperty("type")]
        public GreToClientMessageType Type => GreToClientMessageType.GREMessageType_GameStateMessage;

        [JsonProperty("gameStateMessage")]
        public GameStateMessage GameStateMessage { get; set; }
    }
}
