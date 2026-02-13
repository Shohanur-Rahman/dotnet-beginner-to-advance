using JobFinder.Business.Todo.Interfaces;
using JobFinder.Entities.Todos.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobFinder.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodosController> _logger;
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        // GET: api/<TodosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _todoService.GetTodosAsync());
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _todoService.GetTodoByIdAsync(id));
        }

        // POST api/<TodosController>
        [HttpPost]
        [ProducesResponseType(typeof(TodoViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for todo item with title: {Title}", model.Title);
                return BadRequest(ModelState);
            }

            return Ok(await _todoService.AddTodoAsync(model));
        }

        // PUT api/<TodosController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for todo item with title: {Title}", model.Title);
                return BadRequest(ModelState);
            }

            return Ok(await _todoService.UpdateTodoAsync(model));
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _todoService.DeleteTodoAsync(id));
        }
    }
}
