using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Queries;
using Flashcards.Domain.Interfaces;

namespace Flashcards.Application.Feautures.Decks.Handlers
{
    public class GetDeckByIdHandler : IQueryHandler<GetDeckByIdQuery, DeckDTO>
    {
        private readonly IDeckRepository _deckRepository;

        public GetDeckByIdHandler(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository;
        }

        public async Task<DeckDTO> Handle(GetDeckByIdQuery query, CancellationToken cancellationToken)
        {
            var deck = await _deckRepository.GetByIdAsync(query.DeckId, cancellationToken)
                ?? throw new NotFoundException($"Deck with ID {query.DeckId} not found");

            return new DeckDTO(deck.Id, deck.Name, deck.Tags, deck.CreatedAt);
        }
    }
}
