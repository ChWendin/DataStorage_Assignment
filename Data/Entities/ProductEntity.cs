

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
   
    [Required]
    [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
    [Column(TypeName = "NVARCHAR(100)")]
    public string ProductName { get; set; } = null!;
    
    [Required]
    [Column(TypeName = "DECIMAL(18,2)")]
    public decimal Price { get; set; }
}
