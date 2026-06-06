namespace DeleteReactionBot.Configurations;

public record CacheSettings
{
    public int ReactionLifetimeMinutes { get; init; }
}