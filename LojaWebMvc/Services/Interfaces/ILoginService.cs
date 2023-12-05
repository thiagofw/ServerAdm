using LojaWebMvc.Models;

namespace LojaWebMvc.Services.Interfaces;

public interface ILoginService
{

    Task<List<Login>> FindAllAsync();
    Task<Login> FindByIdAsync(int id);
    Task InsertAsync(Login login);
    Task RemoveAsync(int id);
    Task UpdateAsync(Login login);
    
}