using Newtonsoft.Json;

namespace MythicTracker.Application.MTGA.Models
{
    // TODO: По-хорошему, надо бы вынести Type в интерфейс, и реализовать его через несколько конкретных типов со своими значениями енама.
    public class AnnotationDetail
    {
        [JsonProperty("key")]
        public AnnotationDetailKeyType Key { get; set; }

        [JsonProperty("type")]
        public AnnotationDetailType Type { get; set; }

        [JsonProperty("valueInt32")]
        public int[] ValueInt32 { get; set; }

        [JsonProperty("valueUInt32")]
        public uint[] ValueUInt32 { get; set; }

        [JsonProperty("valueInt64")]
        public long[] ValueInt64 { get; set; }

        [JsonProperty("valueUInt64")]
        public ulong[] ValueUInt64 { get; set; }

        [JsonProperty("valueBool")]
        public bool[] ValueBool { get; set; }

        [JsonProperty("valueString")]
        public string[] ValueString { get; set; }

        [JsonProperty("valueFloat")]
        public float[] ValueFloat { get; set; }

        [JsonProperty("valueDouble")]
        public double[] ValueDouble { get; set; }
    }
}
