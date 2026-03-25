using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly ICommandHandler<CreateDeckCommand, DeckDTO> _createDeckHandler;
        public DecksController(ICommandHandler<CreateDeckCommand, DeckDTO> createDeckHandler)
        {
            _createDeckHandler = createDeckHandler;
        }

        [HttpPost]
        public async Task<ActionResult<DeckDTO>> CreateDeck(CreateDeckCommand command)
        {
            var result = await _createDeckHandler.Handle(command, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}
