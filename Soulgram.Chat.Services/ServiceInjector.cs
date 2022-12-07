using Microsoft.Extensions.DependencyInjection;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.Chat.Services.Interfaces;
using Soulgram.Chat.Services.Services;

namespace Soulgram.Chat.Services;

public static class ServiceInjector
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IChatManagementService, ChatManagementService>();
    }
}