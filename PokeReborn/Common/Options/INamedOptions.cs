using System.ComponentModel.DataAnnotations;

namespace PokeReborn.Common.Options;

public interface INamedOptions
{
    /// <summary>
    /// Gets the name of the options section.
    /// </summary>
    /// <returns>The name of the options section.</returns>
    static abstract string GetSectionName();
}