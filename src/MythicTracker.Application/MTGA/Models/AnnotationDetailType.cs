using System.Runtime.Serialization;

namespace MythicTracker.Application.MTGA.Models
{
    public enum AnnotationDetailType
    {
        [EnumMember(Value = "KeyValuePairValueType_None")]
        None,

        [EnumMember(Value = "KeyValuePairValueType_uint32")]
        UInt32,

        [EnumMember(Value = "KeyValuePairValueType_int32")]
        Int32,

        [EnumMember(Value = "KeyValuePairValueType_uint64")]
        UInt64,

        [EnumMember(Value = "KeyValuePairValueType_int64")]
        Int64,

        [EnumMember(Value = "KeyValuePairValueType_bool")]
        Bool,

        [EnumMember(Value = "KeyValuePairValueType_string")]
        String,

        [EnumMember(Value = "KeyValuePairValueType_float")]
        Float,

        [EnumMember(Value = "KeyValuePairValueType_double")]
        Double,
    }
}
