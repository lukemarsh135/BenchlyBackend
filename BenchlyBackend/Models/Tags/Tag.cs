using BenchlyBackend.Infrastructure.Attributes;
using BenchlyBackend.Models.Locations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenchlyBackend.Models.Tags;

/// <summary>
///     Defines a tag that can be associated with a location.
/// </summary>
[Entity]
public class Tag
{
    /// <summary>
    ///     The tag's unique identifier.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     The Locations associated with the tag.
    /// </summary>
    public List<Location> Locations { get; set; } = [];

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