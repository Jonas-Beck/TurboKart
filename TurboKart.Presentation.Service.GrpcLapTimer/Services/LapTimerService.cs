using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using TurboKart.Presentation.Service.GrpcLapTimer.Hubs;

namespace TurboKart.Presentation.Service.GrpcLapTimer.Services;

public class LapTimerService : LapTimer.LapTimerBase
{

    private readonly ILogger<LapTimerService> _logger;
    private IHubContext<LapTimerHub, ILapTimerClient> _hubContext { get; }

    public LapTimerService(ILogger<LapTimerService> logger, IHubContext<LapTimerHub, ILapTimerClient> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }

    public override Task<Empty> CartCrossed(CartCrossedRequest request, ServerCallContext context)
    {

        //TODO Send request data using SignalR
        _hubContext.Clients.All.ReceiveMessage(request.KartNo, request.Lap, request.LapTime, request.TotalTime);

        return Task.FromResult(new Empty());
    }
}