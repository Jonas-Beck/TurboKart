using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace TurboKart.Presentation.Service.GrpcLapTimer.Services;

public class LapTimerService : LapTimer.LapTimerBase
{

    private readonly ILogger<GreeterService> _logger;

    public LapTimerService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<Empty> CartCrossed(CartCrossedRequest request, ServerCallContext context)
    {
        
        //TODO Send request data using SignalR
        
        return Task.FromResult(new Empty());
    }
}