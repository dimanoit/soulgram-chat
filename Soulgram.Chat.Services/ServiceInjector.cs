using Microsoft.Extensions.DependencyInjection;
using Soulgram.Chat.Services.Interfaces;

namespace Soulgram.Chat.Services;

public static class ServiceInjector
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IChatManagementService, ChatManagementService>();
    }
}