using System.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using PokeReborn.Assets;

namespace PokeReborn.Context;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    
    public DbSet<Pokemon> Pokemons { get; set; }
    
    public DbContext(Microsoft.EntityFrameworkCore.DbContextOptions<DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>().ToCollection("Pokemon");
        modelBuilder.Entity<Pokemon>(entity =>
        {
            entity.Property(e => e.PokedexNumber).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type1).IsRequired();
            entity.Property(e => e.Total).IsRequired();
            entity.Property(e => e.HP).IsRequired();
            entity.Property(e => e.Attack).IsRequired();
            entity.Property(e => e.Defense).IsRequired();
            entity.Property(e => e.SpAtk).IsRequired();
            entity.Property(e => e.SpDef).IsRequired();
            entity.Property(e => e.Speed).IsRequired();
            entity.Property(e => e.Generation).IsRequired();
        });
    }
}