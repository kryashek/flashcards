using Flashcards.Domain.Models;

namespace Flashcards.Domain.Interfaces
{
    public interface IDeckRepository
    {
        Task<Deck?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Deck>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        void Add(Deck deck);
        void Update(Deck deck);
        void Remove(Deck deck);
    }
}
