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
            var decks = await _deckRepository.GetByUserIdAsync(query.UserId, cancellationToken);
            return [.. decks.Select(d => new DeckDTO 
            { 
                Id = d.Id,
                Name = d.Name,
                Tags = d.Tags,
                CreatedAt = d.CreatedAt
            })];
        }
    }
}
