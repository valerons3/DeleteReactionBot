using DeleteReactionBot.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DeleteReactionBot.Infrastructure.BackgroundServices;

public class BotWorker : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IReactionProxyService _reactionProxyService;

    public BotWorker(ITelegramBotClient botClient, IReactionProxyService reactionProxyService)
    {
        _botClient = botClient;
        _reactionProxyService = reactionProxyService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _botClient.StartReceiving(
            updateHandler: HandleUpdate,
            errorHandler: HandleError,
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = new[]
                {
                    UpdateType.MessageReaction
                }
            },
            cancellationToken: stoppingToken);
        
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
    
    private async Task HandleUpdate(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        if (update.Type == UpdateType.MessageReaction)
        {
            await _reactionProxyService.HandleReactionAsync(update, ct);
        }
    }

    private Task HandleError(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        Console.WriteLine(ex);
        return Task.CompletedTask;
    }
}