using System.Diagnostics;
using DeleteReactionBot.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeleteReactionBot.Infrastructure.Services;

public class ReactionHandlerService : IReactionHandlerService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<ReactionHandlerService> _logger;

    public ReactionHandlerService(ITelegramBotClient botClient, ILogger<ReactionHandlerService> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }
    
    public async Task RemoveReactionAsync(Update update, CancellationToken ct)
    {
        var messageReaction = update.MessageReaction!;
        
        _logger.LogInformation("Удаление реакции пользователя {Username} на сообщение {MessageId}",
            messageReaction.User!.Username, messageReaction.MessageId);
        
        await _botClient.DeleteMessageReaction(
            chatId: messageReaction.Chat.Id,
            messageId: messageReaction.MessageId,
            userId: messageReaction.User!.Id,
            cancellationToken: ct);
    }
}