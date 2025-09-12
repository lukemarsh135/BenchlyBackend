using BenchlyBackend.Models.Reviews;
using BenchlyBackend.Models.Tags;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenchlyBackend.Models.Locations;

/// <summary>
///     Location.
/// </summary>
public sealed class Location 
{
    /// <summary>
    ///     The unique identifier for the location.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    /// <summary>
    ///     Images associated with the location.
    /// </summary>
    public ICollection<string>? Images { get; set; }

    /// <summary>
    ///     Tags associated with the location.
    /// </summary>
    public ICollection<Tag>? Tags { get; set; } = [];

    /// <summary>
    ///     Latitude of the location.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    ///     Longitude of the location.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    ///     The reviews associated with the location.
    /// </summary>
    public ICollection<Review>? Reviews { get; set; } = [];

    /// <summary>
    ///     The distance from the current location in miles.
    /// </summary>
    public decimal DistanceFromCurrentLocationMiles { get; set; }

    /// <summary>
    ///     The date and time the location was added.
    /// </summary>
    public DateTime DateAdded { get; set; }

    /// <summary>
    ///     Flag indicating whether the location has been removed.
    /// </summary>
    public bool Removed { get; set; } 
}