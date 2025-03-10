using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models;

public class Task
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}