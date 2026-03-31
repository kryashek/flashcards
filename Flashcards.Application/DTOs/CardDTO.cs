namespace Flashcards.Application.DTOs
{
    public record CardDTO(int Id, int DeckId, DateTime CreatedAt)
    {
        public string Front { get; set; } = string.Empty;
        public string Back { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
