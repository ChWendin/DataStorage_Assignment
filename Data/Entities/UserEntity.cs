
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
   
    [Required]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    [Column(TypeName = "NVARCHAR(50)")]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    [Column(TypeName = "NVARCHAR(50)")]
    public string LastName { get; set; } = null!;
   
    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;
  
    [Required]
    [StringLength(50, ErrorMessage = "User role cannot exceed 50 characters.")]
    [Column(TypeName = "NVARCHAR(50)")]
    public string UserRole { get; set; } = null!;
}

