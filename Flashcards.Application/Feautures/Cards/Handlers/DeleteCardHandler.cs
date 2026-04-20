using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Domain.Interfaces;

namespace Flashcards.Application.Feautures.Cards.Handlers
{
    public class DeleteCardHandler : ICommandHandler<DeleteCardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICardRepository _cardRepository;

        public DeleteCardHandler(ICardRepository cardRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetByIdAsync(command.CardId, cancellationToken)
                ?? throw new NotFoundException($"Card with ID {command.CardId} not found");

            _cardRepository.Remove(card);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
