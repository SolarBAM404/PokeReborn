using Discord.Commands;
using Discord.Interactions;
using RuntimeResult = Discord.Interactions.RuntimeResult;

namespace PokeReborn.Common.Results;

public class InteractionResult(InteractionCommandError? error, string reason) : RuntimeResult(error, reason)
{
    public static InteractionResult Success(string? message = null)
        => new InteractionResult(null, message ?? "Success");

    public static InteractionResult Failure(InteractionCommandError error, string reason)
        => new InteractionResult(error, reason);

    public static InteractionResult Failure(string message)
        => new InteractionResult(InteractionCommandError.Unsuccessful, message);
}