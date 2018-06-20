using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernStore.Domain.Entities;
using ModernStore.Domain.ValueObjects;

namespace ModernStore.Domain.Tests
{
    [TestClass]
    public class CustomerTests
    {
        private readonly Email email = new Email("flp.augusto@gmail.com");
        private readonly Document document = new Document("11122233344");
        private readonly User user = new User("Felipe", "Felipe", "Felipe");

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidFirstNameShouldReturnANotification()
        {
            
            var customer = new Customer("", "Augusto", email, document, user);

            // Se falhar é pq deu certo, pois o teste tem quer dar falha
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidLastNameShouldReturnANotification()
        {
            var customer = new Customer("Felipe", "", email, document, user);
            Assert.IsFalse(customer.IsValid());
        }

        [TestMethod]
        [TestCategory("Customer - New Customer")]
        public void GivenAnInvalidEmailShouldReturnANotification()
        {
            var customer = new Customer("Felipe", "Augusto", email, document, user);
            Assert.IsFalse(customer.IsValid());
        }
    }
}
