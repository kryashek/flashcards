using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Feautures.Decks.Commands
{
    public class CreateDeckCommand
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<string> Tags { get; set; } = [];
    }
}
