using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GreToClientConnectRespMessage : IGreToClientMessage
    {
        [JsonProperty("type")]
        public GreToClientMessageType Type => GreToClientMessageType.GREMessageType_ConnectResp;

        [JsonProperty("connectResp")]
        public ConnectionResponseMessage ConnectionResponseMessage { get; set; }
    }
}
