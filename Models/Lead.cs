using System.ComponentModel.DataAnnotations;

namespace MHAWebDesign.Models;

public class Lead
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(150)]
    public string? Business { get; set; }

    [StringLength(30)]
    public string? Phone { get; set; }

    [Required]
    [StringLength(100)]
    public string ProjectType { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Budget { get; set; }

    [StringLength(100)]
    public string? Timeline { get; set; }

    [Required]
    [StringLength(3000)]
    public string Message { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; }
        = DateTime.UtcNow;

    [Required]
    [StringLength(50)]
    public string Status { get; set; }
        = "New";

    // Tracks whether the notification email
    // was successfully delivered to M.H.A.
    public bool EmailSent { get; set; }
        = false;

    public DateTime? EmailSentAt { get; set; }
}