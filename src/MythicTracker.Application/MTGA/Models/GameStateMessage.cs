namespace MythicTracker.Application.MTGA.Models
{
    public class GameStateMessage
    {
        public GameStateMessageType Type { get; set; }

        public GameObject[] GameObjects { get; set; }

        public Zone[] Zones { get; set; }

        public Annotation[] Annotations { get; set; }
    }
}
