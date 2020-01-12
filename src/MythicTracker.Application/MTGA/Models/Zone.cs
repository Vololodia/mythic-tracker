namespace MythicTracker.Application.MTGA.Models
{
    public class Zone
    {
        public int ZoneId { get; set; }

        public ZoneType Type { get; set; }

        public int[] ObjectInstanceIds { get; set; }
    }
}
