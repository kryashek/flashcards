namespace Flashcards.Application.Feautures.Cards.Commands
{
    public class UpdateCardCommand
    {
        public int Id { get; set; }
        public string Front { get; set; } = string.Empty;
        public string Back { get; set; } = string.Empty;
    }
}
