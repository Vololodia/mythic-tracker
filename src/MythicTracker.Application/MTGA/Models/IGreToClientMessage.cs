using MythicTracker.Application.MTGA.Converters;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    [JsonConverter(typeof(GreToClientMessageConverter))]
    public interface IGreToClientMessage
    {
        GreToClientMessageType Type { get; set; }
    }
}
