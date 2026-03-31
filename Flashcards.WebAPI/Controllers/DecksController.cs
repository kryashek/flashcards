using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Application.Feautures.Decks.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecksController : ControllerBase
    {
        private readonly ICommandHandler<CreateDeckCommand, DeckDTO> _createDeckHandler;
        private readonly ICommandHandler<UpdateDeckCommand, DeckDTO> _updateDeckHandler;
        private readonly ICommandHandler<DeleteDeckCommand, bool> _deleteDeckHandler;
        private readonly ICommandHandler<GetDecksByUserQuery, List<DeckDTO>> _getDecksByUserHandler;

        public DecksController(
            ICommandHandler<CreateDeckCommand, DeckDTO> createDeckHandler,
            ICommandHandler<UpdateDeckCommand, DeckDTO> updateDeckHandler,
            ICommandHandler<GetDecksByUserQuery, List<DeckDTO>> getDecksByUserHandler,
            ICommandHandler<DeleteDeckCommand, bool> deleteDeckHandler)
        {
            _createDeckHandler = createDeckHandler;
            _updateDeckHandler = updateDeckHandler;
            _getDecksByUserHandler = getDecksByUserHandler;
            _deleteDeckHandler = deleteDeckHandler;
        }

        [HttpPost]
        public async Task<ActionResult<DeckDTO>> CreateDeck(CreateDeckCommand command)
        {
            var result = await _createDeckHandler.Handle(command, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeckDTO>> UpdateDeck(int id, UpdateDeckCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _updateDeckHandler.Handle(command, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<DeckDTO>>> GetUserDecks(int userId)
        {
            var query = new GetDecksByUserQuery { UserId = userId };
            return Ok(await _getDecksByUserHandler.Handle(query, HttpContext.RequestAborted));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            var command = new DeleteDeckCommand { Id = id };
            await _deleteDeckHandler.Handle(command, HttpContext.RequestAborted);
            return NoContent();
        }
    }
}