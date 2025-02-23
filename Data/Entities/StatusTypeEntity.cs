
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class StatusTypeEntity
{
    [Key]
    public int StatusId { get; set; }
    
    [Required]
    [StringLength(50, ErrorMessage = "Status name cannot exceed 50 characters.")]
    [Column(TypeName = "NVARCHAR(50)")]
    public string StatusName { get; set; } = null!;
}

