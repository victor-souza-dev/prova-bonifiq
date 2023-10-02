namespace ProvaPub.Repository.Interfaces;

public interface IPaymentMethod
{
    Task<bool> ProcessPaymentAsync(decimal paymentValue, int customerId);
    bool SupportsPaymentMethod(string paymentMethod);
}
