namespace WanderHub.Contract.Shared;
public interface IValidationResult
{
#pragma warning disable IDE0040 // Remove accessibility modifiers
    public static readonly Error ValidationError = new (
#pragma warning restore IDE0040 // Remove accessibility modifiers
        "ValidationError",
        "A validation problem occurred.");

    Error[] Errors { get; }
}
