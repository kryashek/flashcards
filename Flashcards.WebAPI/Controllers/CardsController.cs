using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Application.Feautures.Cards.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICommandHandler<CreateCardCommand, CardDTO> _createCardHandler;
        private readonly ICommandHandler<DeleteCardCommand, bool> _deleteCardHandler;
        private readonly ICommandHandler<UpdateCardCommand, CardDTO> _updateCardHandler;
        private readonly ICommandHandler<GetCardsByDeckQuery, List<CardDTO>> _getCardsByDeckHandler;
        private readonly ICommandHandler<GetCardByIdQuery, CardDTO> _getCardByIdHandler;

        public CardsController(
            ICommandHandler<CreateCardCommand, CardDTO> createCardHandler,
            ICommandHandler<DeleteCardCommand, bool> deleteCardHandler,
            ICommandHandler<UpdateCardCommand, CardDTO> updateCardHandler,
            ICommandHandler<GetCardsByDeckQuery, List<CardDTO>> getCardsByDeckHandler,
            ICommandHandler<GetCardByIdQuery, CardDTO> getCardByIdHandler)
        {
            _createCardHandler = createCardHandler;
            _deleteCardHandler = deleteCardHandler;
            _updateCardHandler = updateCardHandler;
            _getCardByIdHandler = getCardByIdHandler;
            _getCardsByDeckHandler = getCardsByDeckHandler;
        }

        [HttpPost]
        public async Task<ActionResult<DeckDTO>> CreateDeck(CreateCardCommand command)
        {
            var result = await _createCardHandler.Handle(command, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var command = new DeleteCardCommand { CardId = id };
            await _deleteCardHandler.Handle(command, HttpContext.RequestAborted);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeckDTO>> UpdateDeck(int id, UpdateCardCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await _updateCardHandler.Handle(command, HttpContext.RequestAborted);
            return Ok(result);
        }

        [HttpGet("deck/{deckId}")]
        public async Task<ActionResult<List<DeckDTO>>> GetCardsByDeck(int deckId)
        {
            var query = new GetCardsByDeckQuery {  DeckId = deckId };
            return Ok(await _getCardsByDeckHandler.Handle(query, HttpContext.RequestAborted));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<DeckDTO>>> GetCardById(int id)
        {
            var query = new GetCardByIdQuery { Id = id };
            return Ok(await _getCardByIdHandler.Handle(query, HttpContext.RequestAborted));
        }
    }
}
