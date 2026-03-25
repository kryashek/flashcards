using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.DTOs
{
    public class DeckDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
