using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hutech.Domain.Entities;

[Table("Category")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    [Required(ErrorMessage = "Name is required.")]
    public required string? Name { get; set; }

    [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}