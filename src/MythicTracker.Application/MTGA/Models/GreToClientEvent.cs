using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GreToClientEvent
    {
        [JsonProperty("greToClientMessages")]
        public IGreToClientMessage[] GreToClientMessages { get; set; }
    }
}
