using FluentValidator;
using ModernStore.Shared.Entities;
using System;

namespace ModernStore.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem() { }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = product.Price;

            new ValidationContract<OrderItem>(this)
                .IsGreaterThan(x => x.Quantity, 0)
                .IsGreaterThan(x => x.Product.QuantityOnHand, quantity + 1, $"Não temos tantos {product.Title}(s) em estoque.");

            //if (this.IsValid())
            Product.DecreaseQuantity(quantity);
        }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; set; }
        public decimal Price { get; private set; }

        public decimal Total() => Price * Quantity;


        public void AddProduct(Product product, int quantity, decimal price)
        {
            //if (!this.AddProductScopeIsValid(product, price, quantity))
            //    return;

            this.ProductId = product.Id;
            //this.Product = product;
            this.Quantity = quantity;
            this.Price = price;

            // Reserva o estoque
            //this.Product.UpdateQuantityOnHand(this.Product.QuantityOnHand - quantity);
        }
    }
}