using MythicTracker.Application.MTGA.Models;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA
{
    public class LogDeserializer
    {
        public static ServerToClientMessage Deserialize(string content)
        {
            var result = JsonConvert.DeserializeObject<ServerToClientMessage>(
                content,
                new JsonSerializerSettings
                {
                    Error = (sender, @event) => @event.ErrorContext.Handled = true,
                });

            return result?.GreToClientEvent == null ? null : result;
        }
    }
}
