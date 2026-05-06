using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Domain.Interfaces;

namespace Flashcards.Application.Feautures.Cards.Handlers
{
    public class UpdateCardHandler : ICommandHandler<UpdateCardCommand, CardDTO>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCardHandler(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CardDTO> Handle(UpdateCardCommand command, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetByIdAsync(command.Id)
                ?? throw new NotFoundException($"Card with ID {command.Id} not found");

            card.UpdateFrontAndBack(command.Front, command.Back);

            _cardRepository.Update(card);
            await _unitOfWork.SaveChangesAsync();

            return new CardDTO(card.Id, card.DeckId, card.CreatedAt) { Front = card.Front, Back = card.Back, Status = card.GetStatusName() };
        }
    }
}
