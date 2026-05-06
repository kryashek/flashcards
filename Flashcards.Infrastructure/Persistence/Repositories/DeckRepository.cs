using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Persistence.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly AppDbContext _context;

        public DeckRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Deck?> GetByIdAsync(int id,  CancellationToken cancellationToken = default)
        {
            return await _context.Decks
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Deck>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Decks
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public void Add(Deck deck)
        {
            _context.Decks.Add(deck);
        }

        public void Update(Deck deck)
        {
            _context.Entry(deck).State = EntityState.Modified;
        }

        public void Remove(Deck deck)
        {
            _context.Decks.Remove(deck);
        }
    }
}
