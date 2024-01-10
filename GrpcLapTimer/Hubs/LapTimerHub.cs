using Microsoft.AspNetCore.SignalR;

namespace GrpcLapTimer.Hubs;

public class LapTimerHub : Hub<ILapTimerClient>
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.Receivemessage(user, message);
    }
}