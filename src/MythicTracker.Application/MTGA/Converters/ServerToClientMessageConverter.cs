using System;
using MythicTracker.Application.MTGA.Models;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Converters
{
    public class ServerToClientMessageConverter : JsonConverter<ServerToClientMessage>
    {
        public override ServerToClientMessage ReadJson(JsonReader reader, Type objectType, ServerToClientMessage existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, ServerToClientMessage value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
