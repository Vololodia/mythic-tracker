namespace MythicTracker.Application.MTGA.Models
{
    public class Annotation
    {
        public int Id { get; set; }

        public int AffectorId { get; set; }

        public int[] AffectedIds { get; set; }

        public AnnotationType[] Type { get; set; }
    }
}
