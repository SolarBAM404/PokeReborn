namespace PokeReborn.Common.Options;

public class ReferenceOptions : INamedOptions
{
    public static string GetSectionName() => "Reference";

    /// <summary>
    /// The URL of the support server for this app.
    /// </summary>
    public required string SupportServerUrl { get; init; }

    /// <summary>
    /// The URL of the source repository for this app.
    /// </summary>
    public required string SourceRepositoryUrl { get; init; }
}