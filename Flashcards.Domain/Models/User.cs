using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Domain.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public HashCode PasswordHash { get; set; }
    }
}
