using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Feautures.Decks.Handlers
{
    public class UpdateDeckHandler : ICommandHandler<UpdateDeckCommand, DeckDTO>
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDeckHandler(IDeckRepository deckRepository, IUnitOfWork unitOfWork)
        {
            _deckRepository = deckRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeckDTO> Handle(UpdateDeckCommand command, CancellationToken cancellationToken)
        {
            var deck = await _deckRepository.GetByIdAsync(command.Id, cancellationToken);

            if (deck == null)
            {
                throw new NotFoundException($"Deck with ID {command.Id} not found");
            }

            deck.UpdateName(command.Name);
            deck.UpdateTags(command.Tags);

            _deckRepository.Update(deck);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeckDTO(deck.Id, deck.Name, deck.Tags, deck.CreatedAt);
        }
    }
}
