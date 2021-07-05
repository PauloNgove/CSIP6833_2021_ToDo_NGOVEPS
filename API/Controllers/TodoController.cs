using System.Threading.Tasks;
using System.Reflection.Metadata;
using System;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.DTO;
using System.Linq;

namespace API.Controllers
{
    public class TodoController : BaseApiController
    {
        private DataContext _context;
        public TodoController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("api/todoItems/all")]
        public async Task<ActionResult<IEnumerable<TodoDto>>> TodoItems()
        {
            var todos = await _context.Todo.Where(s => s.Username = User.Identity.GetUserName());
            List<TodoDto> todoitems = new List<TodoDto>();
            foreach (var todo in todos)
            {
                todoitems.Add(new TodoDto
                {

                    Id = todo.Id,
                    Task = todo.Task,
                    IsComplete = todo.IsComplete,
                    DueDate = todo.DueDate
                });
            }
            return todoitems;
        }
        [HttpGet("api/todoItems/overdue")]
        public async Task<ActionResult<IEnumerable<TodoDto>>> TodoItems()
        {
            var todos = await _context.Todo.Where(opt =>
             opt.IsComplete == false && opt.DueDate < DateTime.UtcNow && opt.Username = User.Identity.GetUserName());
            List<TodoDto> todoitems = new List<TocoDto>();
            foreach (var todo in todos)
            {
                todoitems.Add(new TodoDto
                {

                    Id = todo.Id,
                    Task = todo.Task,
                    IsComplete = todo.IsComplete,
                    DueDate = todo.DueDate
                });
            }
            return Ok(todoitems);
        }
        [HttpGet("api/todoItems/{id}")]
        public async Task<ActionResult<TodoDto>> ToDoItem(int id)
        {
            var item = _context.Todo.FindAsync(id);
            return new TodoDto
            {
                id = item.Id,
                IsComplete = item.IsComplete,
                Task = item.Task,
                DueDate = item.DueDate
            };

        }
        [HttpPost("api/add-todoitem")]
        public async Task<ActionResult> AddToDoItem(TodoDto todoitem)
        {
            var item = new TodoItem
            {
                IsComplete = todoitem.IsComplete,
                Task = todoitem.Task,
                DueDate = todoitem.DueDate,
                Username = User.Identity.GetUserName()
            };
            _context.Todo.Add(item);

            if (await _context.SaveChangesAsync > 0)
                return Ok();

            return BadRequest("Failed to add new To do Task");

        }
        [HttpPut("api/edit-todoitem", Name = "Edit")]
        public async Task<ActionResult> EditToDoItem(TodoDto todoitem)
        {
            if (todoitem.Username != User.Identity.GetUserName())
                return Unauthorized("Cannot modify this item!");

            _context.Entry(todoitem).State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
                return NoContent();
            return BadRequest("Failed to update item");

        }
        [HttpPut("api/delete-todoitem/{Id}", Name = "Delete")]
        public async Task<ActionResult> DeleteToDoItem(int Id)
        {
            var item = await _context.Todo.FindAsync(Id);
            if (item.Username != User.Identity.GetUserName())
                return Unathorized("Cannot modify this item!");

            if (item == null)
                return NotFound();

            _context.Todo.Remove(item);
            if (await _context.SaveChangesAsync() > 0)
                return Ok();
            return BadRequest("Failed to delete item!");

        }

    }
}