using System;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GameStateMessage
    {
        [JsonProperty("type")]
        public GameStateMessageType Type { get; set; }

        [JsonProperty("gameObjects")]
        public GameObject[] GameObjects { get; set; } = Array.Empty<GameObject>();

        [JsonProperty("zones")]
        public Zone[] Zones { get; set; } = Array.Empty<Zone>();

        [JsonProperty("annotations")]
        public Annotation[] Annotations { get; set; } = Array.Empty<Annotation>();
    }
}
