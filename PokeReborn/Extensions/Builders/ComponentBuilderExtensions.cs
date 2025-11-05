using Discord;

namespace PokeReborn.Extensions.Builders;

public static class ComponentBuilderExtensions
{
    public static ComponentBuilder WithLink(this ComponentBuilder builder, string label, Emote emote, string url)
    {
        ButtonBuilder? button = new ButtonBuilder()
            .WithLabel(label)
            .WithUrl(url)
            .WithStyle(ButtonStyle.Link)
            .WithEmote(emote);
        return builder.WithButton(button);
    }
}