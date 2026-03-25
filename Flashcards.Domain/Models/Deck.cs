using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    public class Deck
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<string> Tags { get; private set; } = [];
        public int UserId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public Deck(string name, int userId)
        {
            Name = name;
            UserId = userId;
            CreatedAt = DateTimeOffset.Now;
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
