using System.ComponentModel.DataAnnotations;

namespace WanderHub.Infrastructure.MessageBus.DependencyInjection.Options;
public class MessageBusOptions
{
    [Required, Range(1, 10)] public int RetryLimit { get; init; }
    [Required, Timestamp] public TimeSpan InitialInterval { get; init; }
    [Required, Timestamp] public TimeSpan IntervalIncrement { get; init; }
}
