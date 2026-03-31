namespace Flashcards.Application.DTOs
{
    public record DeckDTO(int Id, string Name, List<string> Tags, DateTime CreatedAt);
}
