using System.ComponentModel.DataAnnotations;

namespace Hutech.Application.ViewModels;

public record CategoryVm(
    [property: Key]
    int Id,

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    string Name,

    [StringLength(100, ErrorMessage = "Description must be less than 100 characters")]
    string Description
);