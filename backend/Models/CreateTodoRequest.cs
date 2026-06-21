using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class CreateTodoRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MinLength(1, ErrorMessage = "Title cannot be empty.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public required string Title { get; set; }
}
