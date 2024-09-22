using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using Microsoft.EntityFrameworkCore;


namespace DisasterManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly DisasterContext _context;

        public TaskController(DisasterContext context)
        {
            _context = context;
        }

        // Get all tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    
                }).ToListAsync();

            return Ok(tasks);
        }

        // Get a task by id
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound("Task not found");

            var taskDto = new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                
            };

            return Ok(taskDto);
        }

        // Create a new task
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            var task = new Models.Task
            {
                Name = taskCreateDto.Name,
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var taskDto = new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskDto);
        }

        // Update a task
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskCreateDto taskUpdateDto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound("Task not found");

            task.Name = taskUpdateDto.Name;
            

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return NotFound("Task not found");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}