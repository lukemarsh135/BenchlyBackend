using BenchlyBackend.Models.Locations;
using System.Text.Json.Serialization;

namespace BenchlyBackend.Models.Tags;

/// <summary>
///     Defines a tag that can be associated with a location.
/// </summary>
public sealed class Tag
{
    /// <summary>
    ///     The tag's unique identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     The Location associated with the tag (foreign key).
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    ///     The navigation property to the Location that this tag is associated with.
    /// </summary>
    [JsonIgnore]
    public Location? Location { get; set; } = default!;

    /// <summary>
    ///     The tags title or name.
    ///     For example, "Scenic", "Waterfront" or "Busy".
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     Flag indicating whether the tag has been removed.
    /// </summary>
    public bool Removed { get; set; } 
}