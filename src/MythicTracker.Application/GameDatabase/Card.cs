using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MythicTracker.Application.GameDatabase
{
    public class Card
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
