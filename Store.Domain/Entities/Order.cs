using System;
using System.Collections.Generic;
using Store.Domain.Enums;
namespace Store.Domain.Entities;

public class Order
{
    public Order(Costumer customer, decimal deliveryFee, Discount discount)
    {
        Customer = customer;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString().Substring(0, 8);
        Status = EOrderStatus.WaitingPayment;
        DeliveryFee = deliveryFee;
        Discount = discount;
        Items = new List<OrderItem>();
    }
    public Costumer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public EOrderStatus Status { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public Discount Discount { get; private set; }
    public List<OrderItem> Items { get; private set; }

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);
        Items.Add(item);
    }
    public decimal Total()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total += item.Total();
        }
        total += DeliveryFee;
        if (Discount != null && Discount.IsValid())
        {
            total -= Discount.Value() : 0;
        }
        return total;
    }
    public void Pay()
    {
        if (amount == Total())
            this.Status = EOrderStatus.WaitingDelivery;
    }
    public void Cancel()
    {
        this.Status = EOrderStatus.Canceled;
    }
    public void Deliver()
    {
        this.Status = EOrderStatus.Delivered;
    }
}