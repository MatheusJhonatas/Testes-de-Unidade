using Flunt.Notifications;
//using Flunt.Validations;
using System;

namespace Store.Domain.Entities;

public class OrderItem : Flunt.Notifications.Notifiable
{
    public OrderItem(Product product, int quantity)
    {
        AddNotifications(new Contract().Requires()
            .IsNotNull(product, "Product", "Product inválido")
            .IsGreaterThan(quantity, 0, "Quantity", "Quantidade deve ser maior que zero"));
        Product = product;
        Price = Product != null ? Product.Price : 0;
        Quantity = quantity;
    }
    public Product Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public decimal Total()
    {
        return Price * Quantity;
    }
}