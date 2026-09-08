using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Cards.Commands;
using Flashcards.Application.Feautures.Cards.Queries;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Application.Feautures.Decks.Queries;
using Flashcards.Domain.Interfaces;
using Flashcards.WebAPI;
using Flashcards.WebAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Flashcards.IntegrationTests.Flashcards.IntegrationTests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void ResolveServices()
        {
            var builder = WebApplication.CreateBuilder();

            // 1. Подключаем ваши реальные production-регистрации
            builder.Services.AddServices("DefaultConnection");

            // 2. Включаем строгую проверку контейнера
            builder.Host.UseDefaultServiceProvider((context, options) =>
            {
                // Проверяет, что все зависимости в графе могут быть разрешены (нет отсутствующих регистраций)
                options.ValidateOnBuild = true;

                // Проверяет, что Scoped-сервисы не внедряются в Singleton-сервисы (ошибка Captive Dependency)
                options.ValidateScopes = true;
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<CardsController>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<DecksController>().Should().NotBeNull();

                scope.ServiceProvider.GetRequiredService<ICardRepository>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<IDeckRepository>().Should().NotBeNull();

                scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateDeckCommand, DeckDTO>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateDeckCommand, DeckDTO>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<ICommandHandler<DeleteDeckCommand, bool>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<IQueryHandler<GetDeckByIdQuery, DeckDTO>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<IQueryHandler<GetDecksByUserQuery, List<DeckDTO>>>().Should().NotBeNull();

                scope.ServiceProvider.GetRequiredService<ICommandHandler<CreateCardCommand, CardDTO>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<ICommandHandler<UpdateCardCommand, CardDTO>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<ICommandHandler<DeleteCardCommand, bool>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<IQueryHandler<GetCardsByDeckQuery, List<CardDTO>>>().Should().NotBeNull();
                scope.ServiceProvider.GetRequiredService<IQueryHandler<GetCardByIdQuery, CardDTO>>().Should().NotBeNull();
            }
        }
    }
}
