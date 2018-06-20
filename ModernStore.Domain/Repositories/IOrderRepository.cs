using ModernStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
