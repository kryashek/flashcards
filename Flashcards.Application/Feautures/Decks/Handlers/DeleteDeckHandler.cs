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
    public class DeleteDeckHandler : ICommandHandler<DeleteDeckCommand, bool>
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeckHandler(IDeckRepository deckRepository, IUnitOfWork unitOfWork)
        {
            _deckRepository = deckRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteDeckCommand command, CancellationToken cancellationToken)
        {
            var deck = await _deckRepository.GetByIdAsync(command.Id, cancellationToken)
                ?? throw new NotFoundException($"Deck with ID {command.Id} not found");

            _deckRepository.Remove(deck);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
