using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer Get(string document)
    {
        if (document == "12345678901")
        {
            return new Customer("John Doe", "johndoe@example.com");
        }

        return null;
    }
}