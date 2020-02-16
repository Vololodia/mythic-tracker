using System;
using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class Zone
    {
        [JsonProperty("zoneId")]
        public int ZoneId { get; set; }

        [JsonProperty("type")]
        public ZoneType Type { get; set; }

        [JsonProperty("ownerSeatId")]
        public int? OwnerSeatId { get; set; }

        [JsonProperty("objectInstanceIds")]
        public int[] ObjectInstanceIds { get; set; } = Array.Empty<int>();
    }
}
