using Telegram.Bot.Types;

namespace DeleteReactionBot.Infrastructure.Services.Interfaces;

public interface IReactionHandlerService
{
    Task RemoveReactionAsync(Update update, CancellationToken ct);
}