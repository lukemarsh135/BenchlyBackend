namespace BenchlyBackend.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public sealed class RegistrationAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
{
    public ServiceLifetime Lifetime { get; } = lifetime;
}
