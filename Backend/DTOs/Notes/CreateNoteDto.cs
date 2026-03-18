using System.ComponentModel.DataAnnotations;

namespace SimpleNoteApp.DTOs.Notes;

public class CreateNoteDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;
}
