
using ModernStore.Domain.Commands;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidator;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;

namespace ModernStore.Domain.Commands.Handlers
{
    public class OrderCommandHandler : Notifiable, ICommandHandler<RegisterOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(RegisterOrderCommand command)
        {
            // Re-hidratando Customer
            var customer = _customerRepository.Get(command.CustomerId);
            var order = new Order(customer, command.DeliveryFee, command.Discount);

            foreach (var item in command.Items)
            {
                // Re-hidratando Product
                var product = _productRepository.Get(item.ProductId);

                var orderItem = new OrderItem();
                orderItem.AddProduct(product, item.Quantity, product.Price);
                order.AddItem(orderItem);
            }

            AddNotifications(order.Notifications);

            if (order.IsValid())
                _orderRepository.Save(order);

            return new RegisterOrderCommandResult(order.Id);
        }
    }
}
