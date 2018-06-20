using System;
using System.Collections.Generic;
using System.Text;

namespace ModernStore.Shared.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        //void Handle(T command);
        ICommandResult Handle(T command);
    }
}
