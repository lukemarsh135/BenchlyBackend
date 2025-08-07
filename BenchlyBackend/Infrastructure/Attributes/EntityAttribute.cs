namespace BenchlyBackend.Infrastructure.Attributes;

/// <summary>
///     The attribute used to mark a class as an entity in the database.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class EntityAttribute : Attribute
{
}
