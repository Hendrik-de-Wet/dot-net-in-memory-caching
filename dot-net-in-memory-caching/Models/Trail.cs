using System.ComponentModel.DataAnnotations;

namespace dot_net_core_in_memory_caching.Models
{
  public class Trail
  {
    /// Gets or sets the Id value of the hiking trail.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name value of the hiking trail.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the hiking trail.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the description of the hiking trail.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// Gets or sets the region of the hiking trail.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the country of the hiking trail.
    /// </summary>
    public string? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the description of the hiking trail.
    /// </summary>
    public string? Longitude { get; set; }
  }
}
