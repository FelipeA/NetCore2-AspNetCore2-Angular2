using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Repositories
{
    public interface IEmailService
    {
        void Send(string name, string email, string subject, string body);
    }
}
