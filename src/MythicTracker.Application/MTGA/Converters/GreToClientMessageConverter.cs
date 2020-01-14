using System;
using MythicTracker.Application.MTGA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MythicTracker.Application.MTGA.Converters
{
    public class GreToClientMessageConverter : JsonConverter<IGreToClientMessage>
    {
        public override IGreToClientMessage ReadJson(JsonReader reader, Type objectType, IGreToClientMessage existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            IGreToClientMessage result = new GreToClientEmptyMessage();

            var jObject = JObject.Load(reader);
            var typeToken = jObject["type"];
            if (Enum.TryParse(typeToken.ToString(), out GreToClientMessageType messageType))
            {
                if (messageType == GreToClientMessageType.GREMessageType_ConnectResp)
                {
                    var connectRespToken = jObject["connectResp"];
                    result = new GreToClientConnectRespMessage
                    {
                        ConnectionResponseMessage = serializer.Deserialize<ConnectionResponseMessage>(connectRespToken.CreateReader()),
                    };
                }
                else if (messageType == GreToClientMessageType.GREMessageType_GameStateMessage)
                {
                    var gameStateMessageToken = jObject["gameStateMessage"];
                    result = new GreToClientGameStateMessage
                    {
                        GameStateMessage = serializer.Deserialize<GameStateMessage>(gameStateMessageToken.CreateReader()),
                    };
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, IGreToClientMessage value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
