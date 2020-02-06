using Newtonsoft.Json;
using System.Collections.Generic;

namespace MythicTracker.Application.GameDatabase
{
    public class DatabaseModel
    {
        [JsonProperty("cards")]
        public Dictionary<int, Card> Cards { get; set; }
    }
}
