using JobFinderWebApp.DB;
using JobFinderWebApp.Models;
using JobFinderWebApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFinderWebApp.Repository.Implementation
{
    public class TodoRepository : ITodoRepository
    {
        #region Properties and Fields

        private readonly JobFinderDbContext _context;

        #endregion


        #region Constructor

        public TodoRepository(JobFinderDbContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods Implementation

        /// <summary>
        /// Get all todo items from the database and return them as a list of TodoViewModel objects.
        /// </summary>
        /// <returns>List of todos</returns>
        public async Task<List<TodoViewModel>> GetTodosAsync()
        {
            return await _context.TodoItems.Select(t => new TodoViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Details = t.Details
            }).ToListAsync();
        }

        /// <summary>
        /// Get a single todo item by its ID from the database and return it as a TodoViewModel.
        /// </summary>
        /// <param name="id">The primary id of todo entity</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TodoViewModel?> GetTodoByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            var todo = await _context.TodoItems.FindAsync(id);

            if (todo == null)
                return null;

            return new TodoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Details = todo.Details ?? ""
            };
        }


        /// <summary>
        /// Add a new todo item to the database using the provided TodoViewModel. 
        /// The method validates the input model and throws exceptions if the model is null or if the title is invalid. If the model is valid, it creates a new DbTodoItem, saves it to the database, and returns a TodoViewModel representing the newly created todo item.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TodoViewModel> AddTodoAsync(TodoViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "TodoViewModel cannot be null.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentException("Title cannot be null or whitespace.", nameof(model.Title));


            var todo = new DbTodoItem
            {
                Title = model.Title,
                Details = model.Details
            };

            await _context.TodoItems.AddAsync(todo);
            await _context.SaveChangesAsync();

            return new TodoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Details = todo.Details
            };
        }


        /// <summary>
        /// Update an existing todo item in the database using the provided TodoViewModel. 
        /// The method validates the input model and throws exceptions if the model is null, if the title is invalid, or if the ID is not valid. 
        /// If the model is valid, it retrieves the existing todo item from the database, updates its properties with the values from the provided model, saves the changes to the database, and returns a TodoViewModel representing the updated todo item.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<TodoViewModel> UpdateTodoAsync(TodoViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "TodoViewModel cannot be null.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentException("Title cannot be null or whitespace.", nameof(model.Title));

            if (model.Id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(model.Id));

            var todo = await _context.TodoItems.FindAsync(model.Id);
            if (todo == null)
                throw new KeyNotFoundException($"Todo item with id {model.Id} not found.");

            // Update the properties of the existing todo item with the values from the provided model
            todo.Title = model.Title;
            todo.Details = model.Details;

            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync();

            return new TodoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Details = todo.Details
            };
        }


        /// <summary>
        /// Deletes the todo item with the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to delete. Must be greater than zero.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the item was
        /// successfully deleted.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="id"/> is less than or equal to zero.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if no todo item with the specified <paramref name="id"/> exists.</exception>
        public async Task<bool> DeleteTodoAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            var todo = await _context.TodoItems.FindAsync(id);

            if (todo == null)
                throw new KeyNotFoundException($"Todo item with id {id} not found.");

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

    }
}
