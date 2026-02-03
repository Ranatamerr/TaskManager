using System.Data;
using System;
using System.ComponentModel.DataAnnotations;
namespace TaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }= DateTime.UtcNow;
}