using Discord;

namespace PokeReborn.Common.Embeds;

public abstract class EmbedBase
{
    public abstract string Name { get; }

    public abstract string? IconUrl { get; }

    public abstract Color Color { get; }
}