using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.DataServices
{
  public interface IRestDataService
  {
    Task<List<ToDo>> GetAllToDosAsync();

    Task AddToDoAsync(ToDo data);
    
    Task DeleteToDoAsync(int id);

    Task UpdateToDoAsync(ToDo data);
  }
}
