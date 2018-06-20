using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class OrderTests
    {
        private readonly Email email = new Email("flp.augusto@gmail.com");
        private readonly Document document = new Document("11122233344");
        private readonly User user = new User("Felipe", "Felipe", "Felipe");

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnOutOfStockProductItShouldReturnAnError()
        {
            var customer = new Customer("Felipe", "Augusto", email, document, user);
            var mouse = new Product("Mouse", 299, "mouse.jpg", 0);

            var order = new Order(customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            // Se falhar é pq deu certo, pois o teste tem quer dar falha
            Assert.IsFalse(order.IsValid());

        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAnInStockProductItShouldUpdateQuantityOnHand()
        {
            var customer = new Customer("Felipe", "Augusto", email, document, user);
            var mouse = new Product("Mouse", 299, "mouse.jpg", 20);

            var order = new Order(customer, 8, 10);
            order.AddItem(new OrderItem(mouse, 2));

            Assert.IsTrue(mouse.QuantityOnHand == 18);

        }

        [TestMethod]
        [TestCategory("Order - New Order")]
        public void GivenAValidOrderTheTotalShouldBe310()
        {
            var customer = new Customer("Felipe", "Augusto", email, document, user);
            var mouse = new Product("Mouse", 300, "mouse.jpg", 20);

            var order = new Order(customer, 12, 2);
            order.AddItem(new OrderItem(mouse, 1));

            Assert.IsTrue(order.Total() == 310);

        }
    }
}
