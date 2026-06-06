using DeleteReactionBot.Configurations;
using DeleteReactionBot.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace DeleteReactionBot.Infrastructure.Services;

public class ReactionProxyService : IReactionProxyService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IReactionHandlerService _reactionHandlerService;
    private readonly CacheSettings _settings;

    public ReactionProxyService(IMemoryCache memoryCache, IReactionHandlerService reactionHandlerService, 
        IOptions<CacheSettings> options)
    {
        _memoryCache = memoryCache;
        _reactionHandlerService = reactionHandlerService;
        _settings = options.Value;
    }

    public async Task HandleReactionAsync(Update update, CancellationToken stoppingToken)
    {
        var key = BuildCashKey(update);

        if (_memoryCache.TryGetValue(key, out _))
        {
            await _reactionHandlerService.RemoveReactionAsync(update, stoppingToken);
            return;
        }

        _memoryCache.Set(key, true, 
            TimeSpan.FromMinutes(_settings.ReactionLifetimeMinutes));
    }
    
    private string BuildCashKey(Update update)
        => update!.MessageReaction!.User!.Id.ToString();
}