using Microsoft.Extensions.Logging;

namespace Globomatics.Infrastructure.Services;

public class FakePaymentService : IPaymentService
{
    private readonly ILogger<IPaymentService> logger;

    public FakePaymentService(ILogger<IPaymentService> logger)
    {
        this.logger = logger;
    }

    public Task<PaymentStatus> GetStatusAsync(Guid orderId)
    {
        logger.LogInformation($"Retrieving payment status for {orderId}");

        return Task.FromResult(PaymentStatus.Processing);
    }
}