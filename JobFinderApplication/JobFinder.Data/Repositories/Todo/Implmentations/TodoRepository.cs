using JobFinder.Data.DB;
using JobFinder.Data.Repositories.Todo.Interfaces;
using JobFinder.Entities.Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Data.Repositories.Todo.Implmentation
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
        public async Task<List<DbTodoItem>> GetTodosAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// Get a single todo item by its ID from the database and return it as a TodoViewModel.
        /// </summary>
        /// <param name="id">The primary id of todo entity</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<DbTodoItem?> GetTodoByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }


        /// <summary>
        /// Add a new todo item to the database using the provided TodoViewModel. 
        /// The method validates the input model and throws exceptions if the model is null or if the title is invalid. If the model is valid, it creates a new DbTodoItem, saves it to the database, and returns a TodoViewModel representing the newly created todo item.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Single Todo Item</returns>
        public async Task<DbTodoItem> AddTodoAsync(DbTodoItem model)
        {

            await _context.TodoItems.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }


        /// <summary>
        /// Update an existing todo item in the database using the provided TodoViewModel. 
        /// The method validates the input model and throws exceptions if the model is null, if the title is invalid, or if the ID is not valid. 
        /// If the model is valid, it retrieves the existing todo item from the database, updates its properties with the values from the provided model, saves the changes to the database, and returns a TodoViewModel representing the updated todo item.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DbTodoItem> UpdateTodoAsync(DbTodoItem model)
        {
            
            
            _context.TodoItems.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }


        /// <summary>
        /// Deletes the todo item with the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to delete. Must be greater than zero.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the item was
        /// successfully deleted.</returns>
        public async Task<bool> DeleteTodoAsync(DbTodoItem model)
        {
            _context.TodoItems.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

    }
}
