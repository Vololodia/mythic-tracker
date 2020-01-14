using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GreToClientEmptyMessage : IGreToClientMessage
    {
        [JsonProperty("type")]
        public GreToClientMessageType Type => GreToClientMessageType.GREMessageType_None;
    }
}
