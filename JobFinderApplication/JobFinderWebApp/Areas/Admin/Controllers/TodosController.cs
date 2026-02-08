using JobFinderWebApp.DB;
using JobFinderWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinderWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TodosController : Controller
    {
        private readonly JobFinderDbContext _context;
        private readonly ILogger<TodosController> _logger;
        public TodosController(JobFinderDbContext context, ILogger<TodosController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var todos = await _context.TodoItems.Select(t => new TodoViewModel 
            { 
                Id = t.Id,
                Title = t.Title,
                Details = t.Details
            }).ToListAsync();
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                var todo = new DbTodoItem
                {
                    Title = model.Title,
                    Details = model.Details
                };
                await _context.TodoItems.AddAsync(todo);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new todo item with id: {Id}", todo.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new todo item with title: {Title}", model.Title);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the todo item. Please try again.");
                return View(model);
            }
        }
    }
}
