namespace Store.Domain.Repositories.Interfaces;

public class FakeDeliveryFeeRepository : IDeliveryFeeRepository
{
    public decimal Get(string zipCoode)
    {
        return 10;
    }
}