using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Chat.Services.Validation.Validators;

namespace Soulgram.Chat.Services;

public static class ServiceInjector
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateChatRequestValidator>();
        services.RegisterAllScopedServices();
    }

    private static void RegisterAllScopedServices(this IServiceCollection services)
    {
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .ToList()
            .ForEach(type => type.RegisterService(services));
    }

    private static void RegisterService(this Type type, IServiceCollection services)
    {
        var typeIsService = type.IsClass && !type.IsAbstract && type.Name.Contains("Service");

        if (typeIsService)
        {
            var interfaceType = type.GetInterfaces().First();
            services.AddScoped(interfaceType, type);
        }
    }
}