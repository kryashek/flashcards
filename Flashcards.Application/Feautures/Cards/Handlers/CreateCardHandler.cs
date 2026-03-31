using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Feautures.Cards.Handlers
{
    public class CreateCardHandler : ICommandHandler<CreateCardCommand, CardDTO>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IDeckRepository _deckRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCardHandler(ICardRepository cardRepository, IDeckRepository deckRepository, IUnitOfWork unitOfWork)
        {
            _cardRepository = cardRepository;
            _deckRepository = deckRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CardDTO> Handle(CreateCardCommand command, CancellationToken cancellationToken)
        {
            var deck = await _deckRepository.GetByIdAsync(command.DeckId, cancellationToken);
            if (deck == null)
                throw new NotFoundException($"Deck with ID {command.DeckId} not found");

            var card = new Card(command.Front, command.Back, command.DeckId);

            _cardRepository.Add(card);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CardDTO(card.Id, card.DeckId, card.CreatedAt)
            {
                Front = card.Front,
                Back = card.Back,
                Status = card.GetStatusName()
            };
        }
    }
}
