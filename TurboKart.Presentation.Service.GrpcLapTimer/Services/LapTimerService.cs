using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace TurboKart.Presentation.Service.GrpcLapTimer.Services;

public class LapTimerService : Greeter.GreeterBase
{
    
    private readonly ILogger<GreeterService> _logger;

    public LapTimerService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public Task<Empty> CartCrossed(CartCrossedRequest request, ServerCallContext context)
    {
        var id = request.KartNo;
        return Task.FromResult(new Empty());
    }
}