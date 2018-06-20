using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Shared.Commands;
using FluentValidator;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;

namespace ModernStore.Domain.Commands.Handlers
{
    public class CustomerCommandHandler : Notifiable, ICommandHandler<RegisterCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(RegisterCustomerCommand command)
        {
            if (_customerRepository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já está cadastrado!");
                return null;
            }

            var email = new ValueObjects.Email(command.Email);
            var document = new ValueObjects.Document(command.Document);
            var user = new User(command.Username, command.Password, command.ConfirmPassword);

            var customer = new Customer(command.FirstName
                , command.LastName
                , email
                , document
                , user
                );

            AddNotifications(email.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(user.Notifications);
            AddNotifications(customer.Notifications);

            if (!customer.IsValid())
                return null;

            _customerRepository.Save(customer);

            _emailService.Send($"{customer.FirstName} {customer.LastName}"
                , customer.Email.Address
                , $"Bem vindo { customer.FirstName }"
                , $"Olá {customer.FirstName}, blá blá blá");

            return new RegisterCustomerCommandResult(customer.Id, $"{customer.FirstName} {customer.LastName}");
        }
    }
}
