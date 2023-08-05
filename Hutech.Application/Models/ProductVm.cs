using System.ComponentModel.DataAnnotations;

namespace Hutech.Application.Models;

public record ProductResponse(
    int Id,
    string Name,
    double Price,
    int Status,
    string? Description,
    string? Image,
    int CategoryId);


public record ProductRequest(
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    [Required(ErrorMessage = "Name is required.")]
    string Name,
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    double Price,
    int Status,
    [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
    string? Description,
    string? Image,
    int CategoryId);