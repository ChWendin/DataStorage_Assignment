using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters.")]
    [Column(TypeName = "NVARCHAR(150)")]
    public string Title { get; set; } = null!;
   

    [Column(TypeName = "DATE")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime EndDate { get; set; }

    //Foreign keys
    [Required]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    [Required]
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;

    [Required]
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    [Required]
    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;
}

