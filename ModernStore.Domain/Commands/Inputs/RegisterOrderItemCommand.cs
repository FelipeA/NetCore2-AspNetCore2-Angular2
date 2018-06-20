using ModernStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Commands.Inputs
{
    public class RegisterOrderItemCommand : ICommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
