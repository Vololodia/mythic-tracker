namespace MythicTracker.Application.MTGA.Models
{
    public class GreToClientGameStateMessage : IGreToClientMessage
    {
        public GreToClientMessageType Type { get; set; }

        public GameStateMessage GameStateMessage { get; set; }
    }
}
