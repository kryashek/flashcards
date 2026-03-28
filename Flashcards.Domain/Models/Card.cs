using Flashcards.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Front { get; set; } = string.Empty;
        public string Back { get; set; } = string.Empty;
        public int DeckId { get; set; }
        public int StatusId { get; set; }
        public int? LastRatingId { get; set; }
        public DateTime? LastReviewDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Deck Deck { get; set; } = null!;
        public Status Status { get; set; } = null!;
        public Rating? LastRating { get; set; }


        public Card(string front, string back, int deckId)
        {
            Front = front;
            Back = back;
            DeckId = deckId;
            CreatedAt = DateTime.Now.ToUniversalTime();
            StatusId = 1;
        }

        private Card() { }

        public void UpdateStatus(int newStatusId, int? newRatingId)
        {
            StatusId = newStatusId;
            LastRatingId = newRatingId;
            LastReviewDate = DateTime.Now.ToUniversalTime();
        }
    }
}
