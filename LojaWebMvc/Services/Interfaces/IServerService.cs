using LojaWebMvc.Models;

namespace LojaWebMvc.Services.Interfaces;

public interface IServerService
{

      Task<List<Server>> FindAllAsync();
      Task<Server> FindAsync(int id);
      Task InsertAsync(Server server);
      Task RemoveAsync(int id);
      Task UpdateAsync(Server server);
      

}