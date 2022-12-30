using Ardalis.Result;
using Aine.Inventory.Core.ProjectAggregate;

namespace Aine.Inventory.Core.Interfaces;

public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}

