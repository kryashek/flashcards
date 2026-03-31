using Flashcards.Application.Common.Interfaces;
using Flashcards.Application.DTOs;
using Flashcards.Application.Feautures.Decks.Commands;
using Flashcards.Application.Feautures.Decks.Handlers;
using Flashcards.Domain.Interfaces;
using Flashcards.Infrastructure.Persistence;
using Flashcards.Infrastructure.Persistence.Repositories;
using Flashcards.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Infrastructure
{
    public static class DependencyInjection
    {
        //public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        //{
        //    // Добавляем DbContext
        //    services.AddDbContext<AppDbContext>(options =>
        //        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        //    // Регистрируем Unit of Work и репозитории
        //    services.AddScoped<IUnitOfWork, UnitOfWork>();
        //    services.AddScoped<IDeckRepository, DeckRepository>();
        //    services.AddScoped<ICardRepository, CardRepository>();

        //    // Регистрируем сервисы
        //    services.AddScoped<ICurrentUserService, CurrentUserService>();
        //    services.AddScoped<IFileStorage, FileStorageService>();
        //    //services.AddScoped<IExportService, ExportService>();

        //    // Регистрируем обработчики команд/запросов (все, что в Application)
        //    // Можно сделать через Reflection или вручную
        //    services.AddScoped<ICommandHandler<CreateDeckCommand, DeckDTO>, CreateDeckHandler>();
        //    //services.AddScoped<IQueryHandler<GetDeckByIdQuery, DeckDto>, GetDeckByIdQueryHandler>();
        //    // ... и так для всех

        //    services.Scan(scan => scan
        //        .FromAssemblies(typeof(CreateDeckCommandHandler).Assembly)
        //        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
        //        .AsImplementedInterfaces()
        //        .WithScopedLifetime()
        //        .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
        //        .AsImplementedInterfaces()
        //        .WithScopedLifetime());

        //    return services;
        //}
    }
}
