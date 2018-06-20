using ModernStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Commands.Results
{
    public class RegisterCustomerCommandResult : ICommandResult
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public RegisterCustomerCommandResult(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
