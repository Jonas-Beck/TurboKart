using Microsoft.AspNetCore.SignalR;
using TurboKart.Presentation.Service.GrpcLapTimer.Hubs;

namespace TurboKart.Presentation.Service.GrpcLapTimer;

public class LapTimerHub : Hub<ILapTimerClient>
{
    public async Task SendMessage(string kartNo, int lap, string lapTime, string totalTime)
    {
        await Clients.All.ReceiveMessage(kartNo, lap, lapTime,  totalTime);
    }
}