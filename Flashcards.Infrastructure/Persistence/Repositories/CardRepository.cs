using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Persistence.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetByDeckIdAsync(int deckId, CancellationToken cancellationToken = default)
        {
            return await _context.Cards
                .Where(c => c.DeckId == deckId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Card?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Cards
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public void Add(Card card)
        {
            _context.Cards.Add(card);
        }

        public void Remove(Card card)
        {
            _context.Cards.Remove(card);
        }

        public void Update(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;
        }
    }
}
