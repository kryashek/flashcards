using FluentValidation;

namespace Flashcards.Application.Feautures.Cards.Commands
{
    public class AddCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public AddCardCommandValidator()
        {
            RuleFor(x => x.DeckId)
                .GreaterThan(0)
                .WithMessage("DeckId must be greater than 0");

            RuleFor(x => x.Front)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Back)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
