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
using Flashcards.WebAPI.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string? connectionString)
        {
            // Add services to the container.
            services.AddControllers()
                .AddApplicationPart(typeof(CardsController).Assembly)
                .AddApplicationPart(typeof(DecksController).Assembly)
                .AddControllersAsServices();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Добавляем DbContext с PostgreSQL
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            // Регистрируем зависимости
            // Domain interfaces -> Infrastructure implementations
            services.AddScoped<IDeckRepository, DeckRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Application handlers (реализуют ICommandHandler<,>)
            services.AddScoped<ICommandHandler<CreateDeckCommand, DeckDTO>, CreateDeckHandler>();
            services.AddScoped<ICommandHandler<UpdateDeckCommand, DeckDTO>, UpdateDeckHandler>();
            services.AddScoped<ICommandHandler<DeleteDeckCommand, bool>, DeleteDeckHandler>();
            services.AddScoped<IQueryHandler<GetDeckByIdQuery, DeckDTO>, GetDeckByIdHandler>();
            services.AddScoped<IQueryHandler<GetDecksByUserQuery, List<DeckDTO>>, GetDecksByUserHandler>();

            services.AddScoped<ICommandHandler<CreateCardCommand, CardDTO>, CreateCardHandler>();
            services.AddScoped<ICommandHandler<UpdateCardCommand, CardDTO>, UpdateCardHandler>();
            services.AddScoped<ICommandHandler<DeleteCardCommand, bool>, DeleteCardHandler>();
            services.AddScoped<IQueryHandler<GetCardsByDeckQuery, List<CardDTO>>, GetCardsByDeckHandler>();
            services.AddScoped<IQueryHandler<GetCardByIdQuery, CardDTO>, GetCardByIdHandler>();
            return services;
        }
    }
}
