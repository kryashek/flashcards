using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Common.Interfaces
{
    public interface ICommandHandler<TCommand, TResult> where TCommand: class
    {
        Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
