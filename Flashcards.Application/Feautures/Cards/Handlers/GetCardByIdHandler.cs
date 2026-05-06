using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Queries;
using Flashcards.Domain.Interfaces;

namespace Flashcards.Application.Feautures.Cards.Handlers
{
    public class GetCardByIdHandler : IQueryHandler<GetCardByIdQuery, CardDTO>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardByIdHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<CardDTO> Handle(GetCardByIdQuery query, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetByIdAsync(query.Id)
                ?? throw new NotFoundException($"Card with ID {query.Id} not found");

            return new CardDTO(card.Id, card.DeckId, card.CreatedAt) { Front = card.Front, Back = card.Back, Status = card.GetStatusName() };
        }
    }
}
