using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class GameObject
    {
        [JsonProperty("instanceId")]
        public int InstanceId { get; set; }

        [JsonProperty("grpId")]
        public int GroupId { get; set; }

        [JsonProperty("type")]
        public GameObjectType Type { get; set; }

        [JsonProperty("zoneId")]
        public int ZoneId { get; set; }

        [JsonProperty("name")]
        public int NameId { get; set; }

        [JsonProperty("parentId")]
        public int? ParentId { get; set; }
    }
}
