using LojaWebMvc.Models;

namespace LojaWebMvc.Services.Interfaces;

public interface ISellerService
{
    Task<List<Seller>> FindAllAsync();
    Task<Seller> FindByIdAsync(int id);
    Task InsertAsync(Seller seller);
    Task RemoveAsync(int id);
    Task UpdateAsync(Seller seller);
    
//
}