using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Entities
{
    public class Product : Shared.Entities.Entity
    {
        public Product(string title, decimal price, string image, int quantityOnHand)
        {
            Title = title;
            Price = price;
            Image = image;
            QuantityOnHand = quantityOnHand;
        }

        protected Product() {
        }

        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public int QuantityOnHand { get; private set; }

        public void DecreaseQuantity(int quantiry) => QuantityOnHand -= quantiry;
    }
}
