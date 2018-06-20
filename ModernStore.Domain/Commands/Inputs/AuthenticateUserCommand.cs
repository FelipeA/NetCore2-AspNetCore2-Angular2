using ModernStore.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Domain.Commands.Inputs
{
    public class AuthenticateUserCommand : ICommand
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
