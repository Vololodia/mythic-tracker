using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MythicTracker.Application.GameDatabase
{
    public class Root
    {
        [JsonProperty("cards")]
        public Dictionary<int, Card> Cards { get; set; }

        //[JsonProperty("abilities")]
        //public Dictionary<int, Card> Abilities { get; set; }
    }
}
