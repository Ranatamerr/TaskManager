using TaskManager.Models;
namespace TaskManager.Services; 

public interface ITaskService
{
    IEnumerable<TaskItem> GetAll(TaskStatus? status);
    TaskItem? GetById(int id);
    TaskItem Create(TaskItem newTask);
    bool Update(int id, TaskItem Task);
    bool Delete(int id);
}