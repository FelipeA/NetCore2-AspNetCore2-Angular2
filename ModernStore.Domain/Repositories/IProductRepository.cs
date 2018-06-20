using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Repositories
{
    public interface IProductRepository
    {
        Product Get(Guid id);

        IEnumerable<GetProductListCommandResult> Get();
    }
}
