using Discord.Addons.Hosting;
using Microsoft.EntityFrameworkCore;
using PokeReborn.Common.Options;
using PokeReborn.Extensions;
using PokeReborn.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DbContext = PokeReborn.Context.DbContext;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true, reloadOnChange: true);

builder.Services.AddNamedOptions<BotParametersOptions>();
builder.Services.AddNamedOptions<ReferenceOptions>();

BotParametersOptions? botParametersOptions = builder.Configuration.GetSection(BotParametersOptions.GetSectionName()).Get<BotParametersOptions>() ?? null;
if (botParametersOptions is null)
    throw new InvalidOperationException("Bot parameters options could not be loaded from configuration.");

builder.Services.AddDiscordHost((config, _) =>
{
    config.SocketConfig = new Discord.WebSocket.DiscordSocketConfig
    {
        LogLevel = Discord.LogSeverity.Info,
        GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
        LogGatewayIntentWarnings = false,
        UseInteractionSnowflakeDate = false,
        AlwaysDownloadUsers = false,
    };
    
    config.Token = botParametersOptions!.Token;
});

builder.Services.AddInteractionService((config, _) =>
{
    config.LogLevel = Discord.LogSeverity.Debug;
    config.DefaultRunMode = Discord.Interactions.RunMode.Async;
    config.UseCompiledLambda = true;
});

builder.Services.AddInteractiveService(config =>
{
    config.LogLevel = Discord.LogSeverity.Warning;
    config.DefaultTimeout = TimeSpan.FromMinutes(5);
    config.ProcessSinglePagePaginators = true;
});
builder.Services.AddHostedService<InteractionHandler>();

builder.Services.AddMongoDB<DbContext>(builder.Configuration.GetConnectionString("MongoDb")!);

IHost host = builder.Build();
await host.RunAsync();