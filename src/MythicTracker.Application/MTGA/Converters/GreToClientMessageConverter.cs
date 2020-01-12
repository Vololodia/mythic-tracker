using System;
using MythicTracker.Application.MTGA.Models;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Converters
{
    public class GreToClientMessageConverter : JsonConverter<GreToClientGameStateMessage>
    {
        public override GreToClientGameStateMessage ReadJson(JsonReader reader, Type objectType, GreToClientGameStateMessage existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, GreToClientGameStateMessage value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
