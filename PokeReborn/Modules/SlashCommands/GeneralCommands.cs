using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.Rest;
using PokeReborn.Assets;
using PokeReborn.Common;
using PokeReborn.Common.Options;
using PokeReborn.Common.Results;
using PokeReborn.Extensions.Builders;
using Microsoft.Extensions.Options;
using Emote = PokeReborn.Assets.Emote;

namespace PokeReborn.Modules.SlashCommands;

public class GeneralCommands(IOptions<ReferenceOptions> options) : ModuleBase
{
    [SlashCommand("ping", "Replies with Pong!")]
    public async Task PingAsync()
    {
        var embed = new EmbedBuilder()
            .WithTitle("Pong!")
            .WithDescription($"Latency: {Context.Client.Latency}ms")
            .WithColor(Colours.Primary)
            .Build();

        await RespondAsync(embed: embed);
    }

    [SlashCommand("about", "Shows information about the app.")]
    public async Task AboutAsync()
    {
        RestApplication? app = await Context.Client.GetApplicationInfoAsync();

        Embed? embed = new EmbedBuilder()
            .WithTitle(app.Name)
            .WithDescription(app.Description)
            .AddField("Servers", Context.Client.Guilds.Count, true)
            .AddField("Latency", Context.Client.Latency + "ms", true)
            .AddField("Version", Assembly.GetExecutingAssembly().GetName().Version, true)
            .WithAuthor(app.Owner.Username, app.Owner.GetDisplayAvatarUrl())
            .WithFooter(string.Join(" · ", app.Tags.Select(t => '#' + t)))
            .WithColor(Colours.Primary)
            .Build();

        MessageComponent? components = new ComponentBuilder()
            .WithLink("Support", emote: Emote.BrandLogos.Discord, url: options.Value.SupportServerUrl)
            .WithLink("Source", emote: Emote.BrandLogos.GitHub, url: options.Value.SourceRepositoryUrl)
            .Build();

        await RespondAsync(embed: embed, components: components);
    }
}