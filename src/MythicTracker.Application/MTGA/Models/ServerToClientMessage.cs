using MythicTracker.Application.MTGA.Converters;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    [JsonConverter(typeof(ServerToClientMessageConverter))]
    public class ServerToClientMessage
    {
        public string TransactionId { get; set; }

        public GreToClientEvent GreToClientEvent { get; set; }
    }
}
