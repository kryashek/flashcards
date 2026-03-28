using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Feautures.Decks.Commands
{
    public class UpdateDeckCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
    }
}
