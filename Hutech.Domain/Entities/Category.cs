using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hutech.Domain.Entities;

[Table("Category")]
public class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}