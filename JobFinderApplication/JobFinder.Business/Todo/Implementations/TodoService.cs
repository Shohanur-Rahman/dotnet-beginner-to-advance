using JobFinder.Business.Todo.Interfaces;
using JobFinder.Data.Repositories.Todo.Interfaces;
using JobFinder.Entities.Todos.DTOs;
using JobFinder.Entities.Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Business.Todo.Implementations
{
    public class TodoService: ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        #region Public Methods

        public async Task<List<TodoViewModel>> GetTodosAsync()
        {
            var todos = await _todoRepository.GetTodosAsync();

            return todos.Select(t => new TodoViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Details = t.Details
            }).ToList();
        }


        public async Task<TodoViewModel?> GetTodoByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            var todo = await _todoRepository.GetTodoByIdAsync(id);

            if (todo == null)
                return null;

            return new TodoViewModel
            {
                Id = todo.Id,
                Title = todo.Title,
                Details = todo.Details ?? ""
            };
        }

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

            var createdTodo = await _todoRepository.AddTodoAsync(todo);

            if(createdTodo == null)
                throw new InvalidOperationException("Failed to create the todo item.");

            return new TodoViewModel
            {
                Id = createdTodo.Id,
                Title = createdTodo.Title,
                Details = createdTodo.Details
            };
        }

        public async Task<TodoViewModel> UpdateTodoAsync(TodoViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "TodoViewModel cannot be null.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentException("Title cannot be null or whitespace.", nameof(model.Title));

            if (model.Id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(model.Id));

            var todo = await _todoRepository.GetTodoByIdAsync(model.Id);
            if (todo == null)
                throw new KeyNotFoundException($"Todo item with id {model.Id} not found.");

            // Update the properties of the existing todo item with the values from the provided model
            todo.Title = model.Title;
            todo.Details = model.Details;

            var updatedTodo = await _todoRepository.UpdateTodoAsync(todo);

            return new TodoViewModel
            {
                Id = updatedTodo.Id,
                Title = updatedTodo.Title,
                Details = updatedTodo.Details
            };
        }


        public async Task<bool> DeleteTodoAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            var todo = await _todoRepository.GetTodoByIdAsync(id);

            if (todo == null)
                throw new KeyNotFoundException($"Todo item with id {id} not found.");

            return await _todoRepository.DeleteTodoAsync(todo);
        }
        #endregion
    }
}
