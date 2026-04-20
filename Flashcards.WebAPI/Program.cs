using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Application.Feautures.Cards.Handlers;
using Flashcards.Application.Feautures.Cards.Queries;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Application.Feautures.Decks.Handlers;
using Flashcards.Application.Feautures.Decks.Queries;
using Flashcards.Domain.Interfaces;
using Flashcards.Infrastructure.Persistence;
using Flashcards.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Добавляем DbContext с PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Регистрируем зависимости
// Domain interfaces -> Infrastructure implementations
builder.Services.AddScoped<IDeckRepository, DeckRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application handlers (реализуют ICommandHandler<,>)
builder.Services.AddScoped<ICommandHandler<CreateDeckCommand, DeckDTO>, CreateDeckHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateDeckCommand, DeckDTO>, UpdateDeckHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteDeckCommand, bool>, DeleteDeckHandler>();
builder.Services.AddScoped<IQueryHandler<GetDeckByIdQuery, DeckDTO>, GetDeckByIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetDecksByUserQuery, List<DeckDTO>>, GetDecksByUserHandler>();

builder.Services.AddScoped<ICommandHandler<CreateCardCommand, CardDTO>, CreateCardHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCardCommand, CardDTO>, UpdateCardHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteCardCommand, bool>, DeleteCardHandler>();
builder.Services.AddScoped<IQueryHandler<GetCardsByDeckQuery, List<CardDTO>>, GetCardsByDeckHandler>();
builder.Services.AddScoped<IQueryHandler<GetCardByIdQuery, CardDTO>, GetCardByIdHandler>();


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

// Применяем миграции при запуске (опционально)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Автоматически создаст/обновит базу данных
}

app.Run();
