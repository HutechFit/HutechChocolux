using System.ComponentModel.DataAnnotations;

namespace Hutech.Application.Models;

public record CategoryResponse(int Id, string Name, string Description);

public record CategoryRequest(
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    string Name,

    [StringLength(100, ErrorMessage = "Description must be less than 100 characters")]
    string Description
    );