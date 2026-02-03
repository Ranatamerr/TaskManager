using TaskManager.Models;
using Microsoft.EntityFrameworkCore;
using TaskManager.Services;
using TaskManager.Data;
public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TaskItem> GetAll(TaskStatus? status)
    {
        var query = _context.TaskItems.AsQueryable(); //adding condition dynamically

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }
        return query.ToList();
    }

    public TaskItem? GetById(int id)
    {
        return _context.TaskItems.Find(id);
    }

    public TaskItem Create(TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;
        task.UpdatedAt = DateTime.UtcNow;

        _context.TaskItems.Add(task);
        _context.SaveChanges();
        return task;
    }
    public bool Update(int id, TaskItem updatedTask)
    {
        var existingTask = _context.TaskItems.Find(id);
        if (existingTask == null)
        {
            return false;
        }

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.DueDate = updatedTask.DueDate;
        existingTask.Priority = updatedTask.Priority;
        existingTask.Status = updatedTask.Status;
        existingTask.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
        return true;
    }
    public bool Delete(int id)
    {
        var task = _context.TaskItems.Find(id);
        if (task == null)
        {
            return false;
        }

        _context.TaskItems.Remove(task);
        _context.SaveChanges();
        return true;
    }
}