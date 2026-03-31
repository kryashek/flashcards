using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Application.Feautures.Cards.Handlers;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Models;
using FluentAssertions;
using Moq;

namespace Flashcards.Tests.Flashcards.UnitTests.Application.Cards
{
    public class AddCardCommandHandlerTests
    {
        private readonly Mock<ICardRepository> _cardRepositoryMock;
        private readonly Mock<IDeckRepository> _deckRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateCardHandler _handler;

        public AddCardCommandHandlerTests()
        {
            _cardRepositoryMock = new Mock<ICardRepository>();
            _deckRepositoryMock = new Mock<IDeckRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateCardHandler(
                _cardRepositoryMock.Object,
                _deckRepositoryMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldAddCardToRepository()
        {
            // Arrange
            var deckId = 1;
            var existingDeck = new Deck("Test Deck", 1);
            typeof(Deck).GetProperty("Id")?.SetValue(existingDeck, deckId);

            var command = new CreateCardCommand
            {
                DeckId = deckId,
                Front = "What is TDD?",
                Back = "Test-Driven Development"
            };

            _deckRepositoryMock
                .Setup(x => x.GetByIdAsync(deckId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingDeck);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _cardRepositoryMock.Verify(x => x.Add(It.IsAny<Card>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            result.Should().NotBeNull();
            result.Front.Should().Be(command.Front);
            result.Back.Should().Be(command.Back);
            result.DeckId.Should().Be(deckId);
            result.Status.Should().Be("New");
        }

        [Fact]
        public async Task Handle_WithValidCommand_ShouldUpdateStatusCard()
        {
            // Arrange


            // Act
            // Assert
        }
    }
}
