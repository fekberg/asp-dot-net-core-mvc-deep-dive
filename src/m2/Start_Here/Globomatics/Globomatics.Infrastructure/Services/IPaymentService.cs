namespace Globomatics.Infrastructure.Services;

public interface IPaymentService
{
    Task<PaymentStatus> GetStatusAsync(Guid orderId);
}