using Flashcards.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    internal class Card
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int DeckId { get; set; }
        public CardStatus Status { get; set; }
        public CardRating LastRating { get; set; }
        public DateTimeOffset LastReviewDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
