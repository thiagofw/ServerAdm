using LojaWebMvc.Models;

namespace LojaWebMvc.Services.Interfaces;

public interface IDepartmentService
{
    Task<List<Department>> FindAllAsync();
}