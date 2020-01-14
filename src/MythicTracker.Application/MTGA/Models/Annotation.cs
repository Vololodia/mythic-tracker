using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    public class Annotation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("affectorId")]
        public int AffectorId { get; set; }

        [JsonProperty("affectedIds")]
        public int[] AffectedIds { get; set; }

        [JsonProperty("type")]
        public AnnotationType[] Type { get; set; }
    }
}
