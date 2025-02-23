

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int CustomerId { get; set; }
   
    [Required]
    [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
    [Column(TypeName = "NVARCHAR(100)")]
    public string CustomerName { get; set; } = null!;
}
