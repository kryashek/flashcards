using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Interfaces
{
    internal interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
