namespace TurboKart.Presentation.Service.GrpcLapTimer.Hubs;

public interface ILapTimerClient
{
    Task Receivemessage(string user, string messege);
}