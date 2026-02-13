using JobFinder.Entities.Todos.DTOs;
using JobFinder.Entities.Todos.Models;
using System;

namespace JobFinder.Data.Repositories.Todo.Interfaces
{
    public interface ITodoRepository
    {
        #region Methods Signatures

        /// <summary>
        /// Get all todo items from the database and return them as a list of TodoViewModel objects.
        /// </summary>
        /// <returns>List of todos</returns>
        Task<List<DbTodoItem>> GetTodosAsync();

        /// <summary>
        /// Get a single todo item by its ID from the database and return it as a TodoViewModel.
        /// </summary>
        /// <param name="id">The primary id of todo entity</param>
        /// <returns></returns>
        Task<DbTodoItem?> GetTodoByIdAsync(int id);

        /// <summary>
        /// Add a new todo item to the database using the provided TodoViewModel. 
        /// The method validates the input model and throws exceptions if the model is null or if the title is invalid. If the model is valid, it creates a new DbTodoItem, saves it to the database, and returns a TodoViewModel representing the newly created todo item.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DbTodoItem> AddTodoAsync(DbTodoItem model);

        /// <summary>
        /// Update an existing todo item in the database using the provided TodoViewModel. 
        /// The method validates the input model and throws exceptions if the model is null, if the title is invalid, or if the ID is not valid. 
        /// If the model is valid, it retrieves the existing todo item from the database, updates its properties with the values from the provided model, saves the changes to the database, and returns a TodoViewModel representing the updated todo item.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DbTodoItem> UpdateTodoAsync(DbTodoItem model);

        /// <summary>
        /// Deletes the todo item with the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to delete. Must be greater than zero.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the item was
        /// successfully deleted.</returns>
        Task<bool> DeleteTodoAsync(DbTodoItem model);

        #endregion
    }
}
