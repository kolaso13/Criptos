using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace Cripto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            Random NumRandom = new Random();
            foreach (var i in _context.TodoItems){
                i.Valor = (decimal)NumRandom.Next(0,101);
                if (i.Valor_Maximo < i.Valor){
                    i.Valor_Maximo = i.Valor;
                }
            }
            _context.SaveChanges();
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(string Nombre)
        {
            var todoItem = await _context.TodoItems.FindAsync(Nombre);

            Random NumRandom = new Random();
            if (todoItem == null){
                return NotFound();
            }
            else{
                todoItem.Valor = (decimal)NumRandom.Next(0,101);
                if (todoItem.Valor_Maximo < todoItem.Valor){
                    todoItem.Valor_Maximo = todoItem.Valor;
                }
            }
            _context.SaveChanges();
            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(string Nombre, TodoItem todoItem)
        {
            if (Nombre != todoItem.Nombre)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(Nombre))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { Nombre = todoItem.Nombre }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(string Nombre)
        {
            var todoItem = await _context.TodoItems.FindAsync(Nombre);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(string Nombre)
        {
            return _context.TodoItems.Any(e => e.Nombre == Nombre);
        }
    }
}
