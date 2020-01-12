namespace MythicTracker.Application.MTGA.Models
{
    public class ConnectionResponseMessage
    {
        public ConnectionStatus Status { get; set; }

        public DeckMessage DeckMessage { get; set; }
    }
}
