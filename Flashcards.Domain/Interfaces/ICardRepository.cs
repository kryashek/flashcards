using Flashcards.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Interfaces
{
    public interface ICardRepository
    {
        Task<Card?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Card>> GetByDeckIdAsync(int deckId, CancellationToken cancellationToken = default);
        void Add(Card deck);
        void Update(Card deck);
        void Remove(Card deck);
    }
}
