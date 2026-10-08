using System.ComponentModel.DataAnnotations;

namespace MHAWebDesign.Models;

public class ContactViewModel
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;


    [StringLength(150)]
    public string? Business { get; set; }


    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [StringLength(30)]
    public string? Phone { get; set; }


    [Required(ErrorMessage = "Please select a project type.")]
    public string ProjectType { get; set; } = string.Empty;


    public string? Budget { get; set; }


    public string? Timeline { get; set; }


    [Required(ErrorMessage = "Please tell us about your project.")]
    [StringLength(
        3000,
        MinimumLength = 20,
        ErrorMessage = "Please provide at least 20 characters about your project."
    )]
    public string Message { get; set; } = string.Empty;
}