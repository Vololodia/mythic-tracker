using System;
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
        public AnnotationType[] Types { get; set; }

        [JsonProperty("details")]
        public AnnotationDetail[] Details { get; set; } = Array.Empty<AnnotationDetail>();
    }
}
