using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Enums
{
    public enum EnumOrderStatus
    {
        Created = 1,
        InProgress = 2,
        OutForDelivery = 3,
        Delivered = 4,
        Canceled = 5
    }
}
