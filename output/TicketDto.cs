using System.ComponentModel.DataAnnotations;

public class TicketDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;
}