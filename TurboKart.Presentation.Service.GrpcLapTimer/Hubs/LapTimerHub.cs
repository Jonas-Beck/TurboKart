using Microsoft.AspNetCore.SignalR;
using TurboKart.Presentation.Service.GrpcLapTimer.Hubs;

namespace TurboKart.Presentation.Service.GrpcLapTimer;

public class LapTimerHub : Hub<ILapTimerClient>
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.Receivemessage(user, message);
    }
}