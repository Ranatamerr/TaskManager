using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;   
using TaskManager.Services; 

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] TaskStatus? status)
        {
            var tasks = _taskService.GetAll(status);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _taskService.GetById(id);
            if (task == null)
            {
                return NotFound(new { message = $"No task exists with ID {id}" });
            }
            return Ok(task);
        }
        [HttpPost]
        public IActionResult Create([FromBody] TaskItem newTask)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState
                .Where(ms => ms.Value != null && ms.Value.Errors.Count > 0)
                .SelectMany(ms => ms.Value!.Errors.Select(err => $"{ms.Key} is required"))
                .ToList();
                return BadRequest(new { message = string.Join(", ", errors) });
            }
             

            var createdTask = _taskService.Create(newTask);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskItem updatedTask)
        {
             if (!ModelState.IsValid)
            {
                var errors = ModelState
                .Where(ms => ms.Value != null && ms.Value.Errors.Count > 0)
                .SelectMany(ms => ms.Value!.Errors.Select(err => $"{ms.Key} is required"))
                .ToList();
                return BadRequest(new { message = string.Join(", ", errors) });
            }

            var updated = _taskService.Update(id, updatedTask);
            if (!updated)
            {
                 return NotFound(new { message = $"No task updated {id}" });
            }
                 return Ok(new { message = "Task updated successfully", task = updatedTask });
        }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _taskService.Delete(id);
        if (!deleted)
        {
                return NotFound(new { message = $"No task exists with ID {id}" });
        }
         return Ok(new { message = "Task deleted successfully", taskId = id });
    }


}