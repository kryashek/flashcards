using Flashcards.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    public class Card
    {
        public int Id { get; private set; }
        public string Front { get; private set; } = string.Empty;
        public string Back { get; private set; } = string.Empty;
        public int DeckId { get; private set; }
        public int StatusId { get; private set; }
        public int? LastRatingId { get; private set; }
        public DateTime? LastReviewDate { get; private set; }
        public DateTime CreatedAt { get; private set; }

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

        public string GetStatusName()
        {
            return StatusId switch
            {
                1 => "New",
                2 => "Learning",
                3 => "Review",
                4 => "Mature",
                _ => "Unknown"
            };
        }
    }
}
