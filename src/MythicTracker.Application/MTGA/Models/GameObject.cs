namespace MythicTracker.Application.MTGA.Models
{
    public class GameObject
    {
        public int InstanceId { get; set; }

        public int GrpId { get; set; }

        public GameObjectType Type { get; set; }

        public int ZoneId { get; set; }

        public int Name { get; set; }

        public int? ParentId { get; set; }
    }
}
