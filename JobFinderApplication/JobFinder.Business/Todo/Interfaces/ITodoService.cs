using JobFinder.Entities.Todos.DTOs;

namespace JobFinder.Business.Todo.Interfaces
{
    public interface ITodoService
    {
        #region Methods Signatures

        Task<List<TodoViewModel>> GetTodosAsync();
        Task<TodoViewModel?> GetTodoByIdAsync(int id);
        Task<TodoViewModel> AddTodoAsync(TodoViewModel model);
        Task<TodoViewModel> UpdateTodoAsync(TodoViewModel model);
        Task<bool> DeleteTodoAsync(int id);

        #endregion
    }
}
