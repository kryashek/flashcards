using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    internal class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
