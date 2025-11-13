using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using PokeReborn.Assets;
using PokeReborn.Common;
using PokeReborn.Context;

namespace PokeReborn.Modules.Spawning;

public class PokeSpawning : ModuleBase
{

    private readonly DbContext _context;
        
    private DateTime _lastSpawnTime = DateTime.UtcNow - TimeSpan.FromMinutes(10);
    private readonly TimeSpan _spawnInterval = TimeSpan.FromMinutes(5);
    
    private readonly int _requiredChatMessages = 2;
    private int _chatMessageCount = 0;
    

    public PokeSpawning(DiscordSocketClient client, DbContext context)
    {
        _context = context;
        client.MessageReceived += message => 
        {
            OnChatMessageReceived(message);
            return Task.CompletedTask;
        };
    }

    [SlashCommand("forcespawn", "Forces a Pokemon to spawn immediately.")]
    public async Task ForceSpawnAsync()
    {
        // Logic to force spawn a Pokemon
        Pokemon pikachu = new Pokemon
        {
            Name = "Pikachu",
            Type1 = "Electric",
            Type2 = null,
            PokedexNumber = 25,
            Total = 320,
            HP = 35,
            Attack = 55,
            Defense = 40,
            SpAtk = 50,
            SpDef = 50,
            Speed = 90,
            Generation = 1,
            Legendary = false
        };

        if (!_context.Pokemons.Any(p => p.PokedexNumber == pikachu.PokedexNumber))
        {
            _context.Pokemons.Add(pikachu);
        }
        _context.SaveChanges();
        
        SpawnPokemon();
        Context.Channel.SendMessageAsync(_context.Pokemons.ToList().Count.ToString());
        await RespondAsync("A wild Pokémon has been forced to spawn!");
    }

    [SlashCommand("pokedex", "Shows details of a Pokémon from the Pokédex.")]
    public async Task PokedexAsync([Summary("name", "The name of the Pokémon to look up.")] string? name = null, 
        [Summary("id", "The Pokédex number of the Pokémon to look up.")] int? id = null)
    {
        if (name == null && id == null)
        {
            await RespondAsync("Please provide either a valid Pokémon name or Pokédex number.");
            return;
        }
        
        Pokemon? pokemon = null;
        
        if (id != null)
        {
            pokemon = _context.Pokemons.FirstOrDefault(p => p.PokedexNumber == id);
        }
        else if (name != null)
        {
            pokemon = _context.Pokemons.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        
        if (pokemon == null)
        {
            await RespondAsync("Pokémon not found in the Pokédex.");
            return;
        }
        
        Embed embed = new EmbedBuilder()
            .WithTitle($"{pokemon.Name} (#{pokemon.PokedexNumber})")
            .WithThumbnailUrl(pokemon.GetPokemonImage())
            .AddField("Type", pokemon.Type2 == null ? pokemon.Type1 : $"{pokemon.Type1} / {pokemon.Type2}", true)
            .AddField("Total Stats", pokemon.Total, true)
            .AddField("HP", pokemon.HP, true)
            .AddField("Attack", pokemon.Attack, true)
            .AddField("Defense", pokemon.Defense, true)
            .AddField("Special Attack", pokemon.SpAtk, true)
            .AddField("Special Defense", pokemon.SpDef, true)
            .AddField("Speed", pokemon.Speed, true)
            .AddField("Generation", pokemon.Generation, true)
            .AddField("Legendary", pokemon.Legendary ? "Yes" : "No", true)
            .WithColor(Color.Blue)
            .Build();
        await RespondAsync(embed: embed);
    }


    public void OnChatMessageReceived(SocketMessage message)
    {
        _chatMessageCount++;
        if (_chatMessageCount >= _requiredChatMessages && DateTime.UtcNow - _lastSpawnTime >= _spawnInterval)
        {
            SpawnPokemon(message.Channel as SocketTextChannel, _context);
            _chatMessageCount = 0;
            _lastSpawnTime = DateTime.UtcNow;
        }
    }

    private void SpawnPokemon(SocketTextChannel channel = null, DbContext context = null)
    {
        long nextInt64 = Random.Shared.NextInt64();
        int position = (int)(nextInt64 % _context.Pokemons.Count());
        Pokemon spawnedPokemon = _context.Pokemons.Skip(position).FirstOrDefault() ?? throw new InvalidOperationException();

        EmbedBuilder embed = new EmbedBuilder()
            .WithTitle("A wild Pokémon has appeared!")
            .WithDescription($"A wild **{spawnedPokemon.Name}** has appeared! Type `/catch {spawnedPokemon.Name}` to catch it!")
            .WithThumbnailUrl(spawnedPokemon.GetPokemonImage())
            .WithColor(Color.Green);
        
        channel.SendMessageAsync(embed: embed.Build());
    }


}