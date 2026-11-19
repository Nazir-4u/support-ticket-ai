using System.ComponentModel.DataAnnotations;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Open";
}