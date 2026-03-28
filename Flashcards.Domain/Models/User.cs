using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<Deck> Decks { get; set; } = [];
    }
}
