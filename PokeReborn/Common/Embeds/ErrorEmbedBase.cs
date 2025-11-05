using Discord;
using PokeReborn.Assets;
using Emote = PokeReborn.Assets.Emote;

namespace PokeReborn.Common.Embeds;

public class ErrorEmbedBase : EmbedBase
{
    public override string Name { get; } = "Error";
    public override string? IconUrl { get; } = Emote.Reactions.Cross.Url;
    public override Color Color { get; } = Colours.Error;
}