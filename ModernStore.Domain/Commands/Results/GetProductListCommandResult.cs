using System;
using System.Collections.Generic;
using System.Text;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Results
{
    public class GetProductListCommandResult : ICommandResult
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
