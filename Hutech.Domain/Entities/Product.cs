using Hutech.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hutech.Domain.Entities;

[Table("Product")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    [Required(ErrorMessage = "Name is required.")]
    public required string? Name { get; set; }

    public double Price { get; set; }

    public Status Status { get; set; }

    [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
    public string? Description { get; set; }

    public string? Image { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
}