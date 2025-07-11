using Store.Domain.Entities;
using Store.Domain.Enums;
namespace Store.Domain.Entities;
[TestClass]
public class OrderTest
{
    private readonly Customer _customer = new Customer("João da Silva", "joao@gmail.com");
    private readonly Product _product = new Product("Produto 1", 8.5m, true);
    private readonly Discount _discount = new Discount(0.1m, DateTime.Now.AddDays(10));
    public void Dado_UmPedidoValido_DeveSerVálido()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 0, null);
        

        // Act é onde você executa a ação que está sendo testada
        var isValid = order.IsValid;

        // Assert é onde você verifica se o resultado é o esperado
        Assert.IsTrue(isValid);
    }
}