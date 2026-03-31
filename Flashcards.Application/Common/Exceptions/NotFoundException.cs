using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Common.Exceptions
{
    public class NotFoundException : ArgumentException
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}
