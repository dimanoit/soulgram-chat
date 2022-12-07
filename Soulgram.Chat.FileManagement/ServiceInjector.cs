using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Chat.Infrastructure.Ports;
using Soulgram.File.Manager;
using Soulgram.File.Manager.Interfaces;
using Soulgram.File.Manager.Models;

namespace Soulgram.Chat.FileManagement;

public static class ServiceInjector
{
    public static void AddFileManager(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IChatFileManager, ChatFileManager>();

        services.Configure<BlobStorageOptions>(options =>
            configuration
                .GetSection("BlobStorageOptions")
                .Bind(options)
        );

        services.AddScoped<IContainerNameResolver, ContainerNameResolver>();
        services.AddScoped<IFileManager, ChatFileManager>();
    }
}