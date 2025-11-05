using System.ComponentModel.DataAnnotations;

namespace PokeReborn.Common.Options;

public class BotParametersOptions : INamedOptions
{
    public static string GetSectionName() => "BotParameters";

    /// <summary>
    /// App token obtained from the https://discord.com/developers/applications.
    /// </summary>
    [Required]
    public required string Token { get; init; }

    /// <summary>
    /// The ID of a server in Discord used for development of this app.
    /// </summary>
    [Required]
    [RegularExpression("[^0].*", ErrorMessage = "The ID must not be zero")]
    public required ulong DevGuildId { get; init; }
}