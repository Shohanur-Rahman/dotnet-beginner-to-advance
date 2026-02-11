using JobFinder.Business.Todo.Interfaces;
using JobFinder.Entities.Todos.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinderWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TodosController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodosController> _logger;
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {

            var todos = await _todoService.GetTodosAsync();

            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoViewModel model)
        {
            _logger.LogInformation("Creating a new todo item with title: {Title}", model.Title);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for todo item with title: {Title}", model.Title);
                return View(model);
            }

            try
            {
                var result = await _todoService.AddTodoAsync(model);

                _logger.LogInformation("Successfully created a new todo item with id: {Id}", result.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new todo item with title: {Title}", model.Title);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the todo item. Please try again.");
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if(id <= 0)
            {
                _logger.LogWarning("Invalid id provided for editing todo item: {Id}", id);
                return BadRequest("Invalid id.");
            }

            try
            {
                var todo = await _todoService.GetTodoByIdAsync(id);
                if (todo == null)
                {
                    _logger.LogWarning("Todo item not found for editing with id: {Id}", id);
                    return NotFound("Todo item not found.");
                }
                return View(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving todo item for editing with id: {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the todo item. Please try again.");
            }
        }

    }
}
