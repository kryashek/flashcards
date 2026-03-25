using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;

namespace Flashcards.Application.Feautures.Decks.Handlers
{
    public class CreateDeckHandler : ICommandHandler<CreateDeckCommand, DeckDTO>
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeckHandler(IDeckRepository deckRepository, IUnitOfWork unitOfWork)
        {
            _deckRepository = deckRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeckDTO> Handle(CreateDeckCommand command, CancellationToken cancellationToken)
        {
            var deck = new Deck(command.Name, command.UserId);
            foreach (var tag in command.Tags)
            {
                deck.AddTag(tag);
            }

            _deckRepository.Add(deck);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeckDTO
            {
                Id = deck.Id,
                Name = deck.Name,
                Tags = deck.Tags,
                CreatedAt = deck.CreatedAt
            };
        }
    }
}
