using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;

        public ICollection<Card> Cards { get; set; } = [];
    }
}
