namespace MythicTracker.Application.MTGA.Models
{
    public enum ConnectionStatus
    {
        ConnectionStatus_None,
        ConnectionStatus_Success,
        ConnectionStatus_GRPVersionIncompat,
        ConnectionStatus_ProtoVersionIncompat,
    }
}
