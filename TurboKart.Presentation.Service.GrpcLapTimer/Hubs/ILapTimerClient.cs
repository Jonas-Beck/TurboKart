namespace TurboKart.Presentation.Service.GrpcLapTimer.Hubs;

public interface ILapTimerClient
{
    Task ReceiveMessage(string kartNo, int lap, string lapTime, string totalTime);
}