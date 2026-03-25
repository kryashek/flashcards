using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;

namespace Flashcards.Infrastructure.Persistence.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly List<Deck> _decks = [];
        private int _nextId = 1;

        public Task<Deck?> GetByIdAsync(int id,  CancellationToken cancellationToken = default)
        {
            var deck = _decks.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(deck);
        }

        public Task<IEnumerable<Deck>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var userDecks = _decks.Where(d => d.UserId == userId);
            return Task.FromResult(userDecks);
        }

        public void Add(Deck deck)
        {
            var id = _nextId++;

            // Используем рефлексию для установки Id (в реальном проекте лучше использовать фабрику или EF Core)
            typeof(Deck).GetProperty("Id")?.SetValue(deck, id);
            _decks.Add(deck);
        }

        public void Update(Deck deck)
        {
            var existing = _decks.FirstOrDefault(d => d.Id == deck.Id);
            if (existing != null)
            {
                _decks.Remove(existing);
                _decks.Add(deck);
            }
        }

        public void Remove(Deck deck)
        {
            _decks.Remove(deck);
        }
    }
}
