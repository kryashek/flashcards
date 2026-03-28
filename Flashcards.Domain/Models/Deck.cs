namespace Flashcards.Domain.Models
{
    public class Deck
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<string> Tags { get; private set; }
        public int UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; set; } = null!;
        public ICollection<Card> Cards { get; set; } = [];

        public Deck(string name, int userId)
        {
            Name = name;
            UserId = userId;
            CreatedAt = DateTime.UtcNow.ToUniversalTime();
            Tags = [];
        }

        // for EF Core
        private Deck() { }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty");

            Name = newName;
        }

        public void AddTag(string tag)
        {
            Tags.Add(tag);
        }
    }
}
