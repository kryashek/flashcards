using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Application.Feautures.Decks.Handlers;
using Flashcards.Domain.Interfaces;
using Flashcards.Infrastructure.Persistence;
using Flashcards.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем зависимости
// Domain interfaces -> Infrastructure implementations
builder.Services.AddScoped<IDeckRepository, DeckRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application handlers (реализуют ICommandHandler<,>)
builder.Services.AddScoped<ICommandHandler<CreateDeckCommand, DeckDTO>, CreateDeckHandler>();

// Можно добавить и другие обработчики по мере необходимости


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
