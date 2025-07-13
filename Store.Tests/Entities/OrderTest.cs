using Store.Domain.Entities;
using Store.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store.Domain.Entities;
[TestClass]
public class OrderTest
{
    private readonly Customer _customer = new Customer("João da Silva", "joao@gmail.com");
    private readonly Product _product = new Product("Produto 1", 10, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(10));
    
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
	[TestMethod]
	public void Dado_um_novo_pedido_valido_seu_total_deve_ser_50()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 10, _discount);//-10 
        order.AddItem(_product, 5);
        // Act é onde você executa a ação que está sendo testada
        var total = order.Total();
        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o total é 50
        Assert.AreEqual(50, total);
    }
	[TestMethod]
	public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
    {
        // Arrange é onde você configura o cenário do teste
		var discountExpired = new Discount(10, DateTime.Now.AddDays(-5));
        var order = new Order(_customer, 10, discountExpired);
        order.AddItem(_product, 5);
        // Act é onde você executa a ação que está sendo testada
        var total = order.Total();
        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o total é 60
        Assert.AreEqual(60, total);
    }
	[TestMethod]
	public void Dado_um_disconto_invalido_o_valor_do_pedido_deve_ser_60()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        // Act é onde você executa a ação que está sendo testada
        var total = order.Total();
        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o total é 60
        Assert.AreEqual(60, total);
    }
	[TestMethod]
	public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 5);
        // Act é onde você executa a ação que está sendo testada
        var total = order.Total();
        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o total é 50
        Assert.AreEqual(50, total);
    }
	[TestMethod]
	public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(_customer, 10, _discount);// 10 de taxa de entrega, _discount de 10 totalizando 0
        order.AddItem(_product, 6); // 6 * 10 = 60
        // Act é onde você executa a ação que está sendo testada
        var total = order.Total();// 60 - 10 (desconto) + 10 (taxa de entrega) = 60
        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o total é 60
        Assert.AreEqual(60, total);
    }
	[TestMethod]
	public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
    {
        // Arrange é onde você configura o cenário do teste
        var order = new Order(null, 10, null);// Cliente inválido, 10 de taxa de entrega, sem desconto
        // Act é onde você executa a ação que está sendo testada
        var isValid = order.IsValid;// Verifica se o pedido é válido
        // Assert é onde você verifica se o resultado é o esperado, exemplo, se o pedido é inválido
        Assert.IsFalse(isValid);// Pedido inválido, pois o cliente é nulo
    }
}