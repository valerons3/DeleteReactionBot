using System.Diagnostics;
using DeleteReactionBot.Infrastructure.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeleteReactionBot.Infrastructure.Services;

public class ReactionHandlerService : IReactionHandlerService
{
    private readonly ITelegramBotClient _botClient;

    public ReactionHandlerService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }
    
    public async Task RemoveReactionAsync(Update update, CancellationToken ct)
    {
        var messageReaction = update.MessageReaction!;
        
        Debug.Write($"Удаление реакции от пользователя: {messageReaction.User.Username}");
        
        await _botClient.DeleteMessageReaction(
            chatId: messageReaction.Chat.Id,
            messageId: messageReaction.MessageId,
            userId: messageReaction.User!.Id,
            cancellationToken: ct);
    }
}