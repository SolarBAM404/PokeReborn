using Discord;
using PokeReborn.Assets;
using Emote = PokeReborn.Assets.Emote;

namespace PokeReborn.Common.Embeds;

public class SuccessEmbedBase : EmbedBase
{
    public override string Name { get; } = "Succeed!";
    public override string? IconUrl { get; } = Emote.Reactions.Check.Url;
    public override Color Color { get; } = Colours.Success;
}