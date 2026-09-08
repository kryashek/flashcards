using Flashcards.Domain.Interfaces;
using Flashcards.WebAPI;
using Flashcards.WebAPI.Controllers;
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
                scope.ServiceProvider.GetService<CardsController>();
                scope.ServiceProvider.GetService<DecksController>();

                scope.ServiceProvider.GetRequiredService<ICardRepository>();
                scope.ServiceProvider.GetRequiredService<IDeckRepository>();
            }
        }
    }
}
