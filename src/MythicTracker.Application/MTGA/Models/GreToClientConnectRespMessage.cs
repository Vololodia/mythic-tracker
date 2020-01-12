using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GreToClientConnectRespMessage : IGreToClientMessage
    {
        public GreToClientMessageType Type { get; set; }

        [JsonProperty("connectResp")]
        public ConnectionResponseMessage ConnectionResponseMessage { get; set; }
    }
}
