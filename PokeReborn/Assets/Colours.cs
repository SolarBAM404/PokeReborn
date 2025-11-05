using Discord;

namespace PokeReborn.Assets;

public static class Colours
{
    /// <summary>
    /// The colour used for successful operations.
    /// </summary>
    public static readonly Color Success = new(0x00FF00); // Green

    /// <summary>
    /// The colour used for error messages or critical issues.
    /// </summary>
    public static readonly Color Error = new(0xFF0000); // Red

    /// <summary>
    /// The colour used for warning messages or alerts.
    /// </summary>
    public static readonly Color Warning = new(0xFFCC00); // Yellow

    /// <summary>
    /// The colour used for informational messages.
    /// </summary>
    public static readonly Color Info = new(0x0000FF); // Blue

    /// <summary>
    /// The colour used for neutral messages or states.
    /// </summary>
    public static readonly Color Neutral = new(0x808080); // Grey


    /// <summary>
    /// The primary colour used for branding or main themes.
    /// </summary>
    public static readonly Color Primary = new(0x5865F2); // Discord Blue

    /// <summary>
    /// The secondary colour used for accents or complementary themes.
    /// </summary>
    public static readonly Color Secondary = new(0x99AAB5); // Discord Grey
}