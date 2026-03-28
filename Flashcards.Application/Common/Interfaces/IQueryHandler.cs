using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Application.Common.Interfaces
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : class
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
