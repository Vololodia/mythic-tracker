using System.Runtime.Serialization;

namespace MythicTracker.Application.MTGA.Models
{
    public enum AnnotationDetailKeyType
    {
        [EnumMember(Value = "orig_id")]
        OriginalId,

        [EnumMember(Value = "new_id")]
        NewId,

        [EnumMember(Value = "zone_src")]
        ZoneSource,

        [EnumMember(Value = "zone_dest")]
        ZoneDestination,

        [EnumMember(Value = "category")]
        Category,

        [EnumMember(Value = "grpid")]
        GroupId,

        [EnumMember(Value = "topIds")]
        TopIds,

        [EnumMember(Value = "bottomIds")]
        BottomIds,
    }
}
