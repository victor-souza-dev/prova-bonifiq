using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository;

public class PixPaymentRepository : IPaymentMethod
{
    public Task<bool> ProcessPaymentAsync(decimal paymentValue, int customerId)
    {
        return Task.FromResult(true);
    }

    public bool SupportsPaymentMethod(string paymentMethod)
    {
        return string.Equals(paymentMethod, "pix", StringComparison.OrdinalIgnoreCase);
    }

}
