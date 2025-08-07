using BenchlyBackend.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenchlyBackend.Models.Reviews;

/// <summary>
///     Defines a review that can be added to a location.
/// </summary>
[Entity]
public class Review 
{
    /// <summary>
    ///     The unique identifier for the location.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     The unique identifier for the location that this review is associated with.
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    ///     The comments associated with the review.
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    ///     The rating (out of 5) given to the location in the review.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    ///     The date and time the review was posted.
    /// </summary>
    public DateTime DatePosted { get; set; }

    /// <summary>
    ///     Flag indicating whether the review has been removed.
    /// </summary>
    public bool Removed { get; set; }
}