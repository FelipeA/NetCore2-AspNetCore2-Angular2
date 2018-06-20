using FluentValidator;
using ModernStore.Domain.ValueObjects;
using System;

namespace ModernStore.Domain.Entities
{
    public class Customer : Shared.Entities.Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public Document Document { get; set; }
        public Email Email { get; private set; }
        public User User { get; private set; }

        protected Customer() { }

        public Customer(string firstName, string lastName, Email email, Document document, User user)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = null;
            Email = email;
            User = user;
            Document = document;

            new ValidationContract<Customer>(this)
                .IsRequired(x => x.FirstName, "Nome é obrigatório.")
                .HasMaxLenght(x => x.FirstName, 60)
                .HasMinLenght(x => x.FirstName, 3)
                
                .IsRequired(x => x.LastName, "Sobrenome é obrigatório.")
                .HasMaxLenght(x => x.LastName, 60)
                .HasMinLenght(x => x.LastName, 3);

            AddNotifications(Email.Notifications);
            AddNotifications(Document.Notifications);

        }

        public void Update(string firstName, string lastName, DateTime? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
