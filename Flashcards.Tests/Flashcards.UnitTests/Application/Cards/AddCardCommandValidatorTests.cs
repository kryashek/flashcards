using Flashcards.Application.Feautures.Cards.Commands;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Tests.Flashcards.UnitTests.Application.Cards
{
    public class AddCardCommandValidatorTests
    {
        [Fact]
        public void Validate_WithEmptyFront_ShouldHaveError()
        {
            // Arrange
            var validator = new AddCardCommandValidator();
            var command = new CreateCardCommand { DeckId = 1, Front = "", Back = "Valid" };

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Front");
        }
    }
}
