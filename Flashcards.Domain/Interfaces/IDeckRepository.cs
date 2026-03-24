using Flashcards.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Interfaces
{
    internal interface IDeckRepository
    {
        Task<Deck?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Deck>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        void Add(Deck deck);
        void Update(Deck deck);
        void Remove(Deck deck);
    }
}
