using Store.Domain.Entities;
using Store.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Domain.Entities;
[TestClass]
public class OrderTest
{
    private readonly Customer _customer = new Customer("João da Silva", "joao@gmail.com");
    private readonly Product _product = new Product("Produto 1", 8.5m, true);
    private readonly Discount _discount = new Discount(0.1m, DateTime.Now.AddDays(10));
    
    [TestMethod]
    public void Dado_UmNovoPedido_Valido_NumeroPedidoTem8Caracteres()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 8, null);
        

        // Act é onde você executa a ação que está sendo testada
        var isValid = order.IsValid;

        // Assert é onde você verifica se o resultado é o esperado
        Assert.AreEqual(8, order.Number.Length);
    }
    [TestMethod]
    public void Dado_UmPedido_SatusDeveSerAguardandoPagamento()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 0, null);

        // Act é onde você executa a ação que está sendo testada
        var status = order.Status;

        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o status é "Aguardando Pagamento"
        Assert.AreEqual(EOrderStatus.WaitingPayment, status);
    }
}