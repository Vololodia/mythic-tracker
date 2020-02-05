using Newtonsoft.Json;
using System.Collections.Generic;

namespace MythicTracker.Application.GameDatabase
{
    public class Root
    {
        [JsonProperty("cards")]
        public Dictionary<int, Card> Cards { get; set; }
    }
}
