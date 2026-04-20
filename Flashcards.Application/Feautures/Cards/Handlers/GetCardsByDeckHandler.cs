using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Queries;
using Flashcards.Domain.Interfaces;

namespace Flashcards.Application.Feautures.Cards.Handlers
{
    public class GetCardsByDeckHandler : IQueryHandler<GetCardsByDeckQuery, List<CardDTO>>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardsByDeckHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<List<CardDTO>> Handle(GetCardsByDeckQuery query, CancellationToken cancellationToken)
        {
            var cards = await _cardRepository.GetByDeckIdAsync(query.DeckId, cancellationToken)
                ?? throw new NotFoundException($"Cards from deck with ID {query.DeckId} not found");

            return [.. cards.Select(card => new CardDTO(card.Id, card.DeckId, card.CreatedAt) { Front = card.Front, Back = card.Back, Status = card.GetStatusName() })];
        }
    }
}
