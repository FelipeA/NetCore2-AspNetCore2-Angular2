using ModernStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Commands.Results
{
    public class RegisterOrderCommandResult : ICommandResult
    {
        public Guid Id { get; private set; }

        public RegisterOrderCommandResult(Guid id)
        {
            Id = id;
        }
    }
}
