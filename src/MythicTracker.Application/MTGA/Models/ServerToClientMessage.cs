using MythicTracker.Application.MTGA.Converters;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    //[JsonConverter(typeof(ServerToClientMessageConverter))]
    public class ServerToClientMessage
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("greToClientEvent")]
        public GreToClientEvent GreToClientEvent { get; set; }
    }
}
