using FluentValidator;
using ModernStore.Domain.Enums;
using ModernStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Entities
{
    public class Order : Entity
    {
        private readonly List<OrderItem> _items;

        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            DeliveryFee = deliveryFee;
            Discount = discount;
            Status = EnumOrderStatus.Created;
            _items = new List<OrderItem>();
            Customer = customer;

            new ValidationContract<Order>(this)
                .IsGreaterThan(x => x.DeliveryFee, -1);
        }   

        public DateTime CreateDate { get; private set; }
        public string Number { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public decimal Discount { get; private set; }
        public Customer Customer { get; private set; }
        public EnumOrderStatus Status { get; private set; }
        public ICollection<OrderItem> Items
        {
            get
            {
                return _items.ToArray();
            }
        }

        public decimal SubTotal()
        {
            decimal total = 0;

            foreach (var item in Items)
                total += item.Total();

            return total;
        }

        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            AddNotifications(item.Notifications);
            if (item.IsValid())
                _items.Add(item);
        }

    }
}
