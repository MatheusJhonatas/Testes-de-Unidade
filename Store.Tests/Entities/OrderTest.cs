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
    public void Dado_um_novo_pedido_valido_numero_pedido_tem_8_caracteres()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 8, null);
        

        // Act é onde você executa a ação que está sendo testada
        var isValid = order.IsValid;

        // Assert é onde você verifica se o resultado é o esperado
        Assert.AreEqual(8, order.Number.Length);
    }
    [TestMethod]
    public void Dado_um_pedido_satus_deve_ser_aguardando_pagamento()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 0, null);

        // Act é onde você executa a ação que está sendo testada
        var status = order.Status;

        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o status é "Aguardando Pagamento"
        Assert.AreEqual(EOrderStatus.WaitingPayment, status);
    }
    [TestMethod]
	public void Dado_um_pagamento_status_deve_ser_aguardando_entrega()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 0, null);
        order.Pay(order.Total());

        // Act é onde você executa a ação que está sendo testada
        var status = order.Status;

        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o status é "Aguardando Entrega"
        Assert.AreEqual(EOrderStatus.WaitingDelivery, status);
    }
	[TestMethod]
	public void Dado_um_pedido_cancelamento_dedido()
	{
		//Arrange é onde você configura o cenário do teste
		var order = new Order(_customer, 0, null);
		order.Cancel();
		//Act é onde você executa a ação que está sendo testada
		var status = order.Status;
		//Assert é onde você verifica se o resultado é o esperado, exemplo, se o status é "Cancelado"
		Assert.AreEqual(EOrderStatus.Canceled, status);
	}
	[TestMethod]
	public void Dado_um_novo_pedido_sem_produto_o_mesmo_nao_deve_ser_adicionado()
    {
        //Arrange é onde você configura o cenário do teste
		var order = new Order(_customer, 0, null);
		//Act é onde você executa a ação que está sendo testada
		order.AddItem(null, 0);
		//Assert é onde você verifica se o resultado é o esperado, exemplo, se o item não foi adicionado
		Assert.AreEqual(order.Items.Count,0);
    }
    [TestMethod]
	public void Dado_um_novo_pedido_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
	{
		// Arrange é onde você configura o cenário do teste
		var order = new Order(_customer, 0, null);
		// Act é onde você executa a ação que está sendo testada	
		order.AddItem(_product, 0);
		//Assert é onde você verifica se o resultado é o esperado, exemplo, se o item não foi adicionado
		Assert.AreEqual(order.Items.Count, 0);
	}
}