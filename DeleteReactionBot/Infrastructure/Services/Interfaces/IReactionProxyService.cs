using Telegram.Bot.Types;

namespace DeleteReactionBot.Infrastructure.Services.Interfaces;

public interface IReactionProxyService
{
    Task HandleReactionAsync(Update update, CancellationToken stoppingToken);
}