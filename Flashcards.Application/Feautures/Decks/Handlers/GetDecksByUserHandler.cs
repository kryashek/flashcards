using Flashcards.Application.Common.Exceptions;
using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Queries;
using Flashcards.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Feautures.Decks.Handlers
{
    public class GetDecksByUserHandler : IQueryHandler<GetDecksByUserQuery, List<DeckDTO>>
    {
        private readonly IDeckRepository _deckRepository;

        public GetDecksByUserHandler(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository;
        }

        public async Task<List<DeckDTO>> Handle(GetDecksByUserQuery query, CancellationToken cancellationToken)
        {
            var decks = await _deckRepository.GetByUserIdAsync(query.UserId, cancellationToken) 
                ?? throw new NotFoundException($"Decks of user with ID {query.UserId} not found"); ;

            return [.. decks.Select(d => new DeckDTO(d.Id, d.Name, d.Tags, d.CreatedAt))];
        }
    }
}
