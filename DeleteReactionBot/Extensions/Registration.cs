using DeleteReactionBot.Configurations;
using DeleteReactionBot.Infrastructure.BackgroundServices;
using DeleteReactionBot.Infrastructure.Services;
using DeleteReactionBot.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace DeleteReactionBot.Extensions;

public static class Registration
{
    public static IServiceCollection RegisterAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfiguration(configuration);
        services.AddBotClient();
        services.AddServices();
        services.AddCache();
        services.AddBackgroundServices();
        
        return services;
    }

    private static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelegramSettings>(
            configuration.GetSection("Telegram"));
        
        services.Configure<CacheSettings>(
            configuration.GetSection("Cache"));
        
        return services;
    }

    private static IServiceCollection AddBotClient(this IServiceCollection services)
    {
        services.AddSingleton<ITelegramBotClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<TelegramSettings>>().Value;
            return new TelegramBotClient(settings.BotToken);
        });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IReactionHandlerService, ReactionHandlerService>();
        services.AddSingleton<IReactionProxyService, ReactionProxyService>();

        return services;
    }
    
    private static IServiceCollection AddCache(this IServiceCollection services)
        => services.AddMemoryCache();

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        => services.AddHostedService<BotWorker>();
}