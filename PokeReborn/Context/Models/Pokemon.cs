using System.ComponentModel.DataAnnotations;
using System.Data;
using MongoDB.Bson;

namespace PokeReborn.Assets;

public class Pokemon
{
    [Key]
    public ObjectId Id { get; set; }
    
    public int PokedexNumber { get; set; }
    public string Name { get; set; }
    
    public string Type1 { get; set; }
    public string? Type2 { get; set; }
    
    public int Total { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpAtk { get; set; }
    public int SpDef { get; set; }
    public int Speed { get; set; }
    public int Generation { get; set; }
    public bool Legendary { get; set; }


    public string GetPokemonImage()
    {
        return $"https://assets.pokemon.com/assets/cms2/img/pokedex/full/{PokedexNumber:D3}.png";
    }
    
}