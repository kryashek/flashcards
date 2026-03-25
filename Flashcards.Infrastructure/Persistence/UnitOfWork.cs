using Flashcards.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // В in-memory нет реальной транзакции, просто возвращаем успех
            return Task.FromResult(1);
        }
    }
}
