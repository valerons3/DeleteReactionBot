namespace DeleteReactionBot.Configurations;

public record TelegramSettings
{
    public string BotToken { get; init; } = string.Empty;
}