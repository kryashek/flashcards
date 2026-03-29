namespace Flashcards.Application.Feautures.Cards.Commands
{
    public class CreateCardCommand
    {
        public int DeckId { get; set; }
        public string Front { get; set; } = string.Empty;
        public string Back { get; set; } = string.Empty;
    }
}
