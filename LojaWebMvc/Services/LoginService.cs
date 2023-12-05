using LojaWebMvc.Models;
using LojaWebMvc.Data;

using LojaWebMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LojaWebMvc.Services.Exceptions;

namespace LojaWebMvc.Services;

public class LoginService : ILoginService
{
        private readonly VsproContext _vsproContext;
        public LoginService(VsproContext vsproContext)
        {
            _vsproContext = vsproContext;
        }
        public  Task<List<Login>> FindAllAsync()
        {
          var list =  _vsproContext.Login;
          var obj =   list.Select(x=> new Login(
                x.Id,
                x.Name,
                x.Username,
                x.Password,
                x.Server
          )).ToListAsync();
            return obj;
      }

      public async Task<Login> FindByIdAsync(int id)
      {
        return await _vsproContext.Login.Include(obj => obj.Server).FirstOrDefaultAsync(x => x.Id == id);
      }

      public async Task InsertAsync(Login login)
      {
        _vsproContext.Add(login);
        await _vsproContext.SaveChangesAsync();
      }

      public async Task RemoveAsync(int id)
      {
        try{
          var obj = await _vsproContext.Login.FindAsync(id);
          _vsproContext.Remove(obj);
          await _vsproContext.SaveChangesAsync();
        }catch(DbUpdateException e)
        {
          throw new IntegrityException(e.Message);
        }
        
      }

      public async Task UpdateAsync(Login login)
      {
        bool hasAny = await _vsproContext.Login.AnyAsync(x=> x.Id == login.Id);

        if(!hasAny)
        {
          throw new NotFoundException("Error - Id not found");
        }
        try{
          _vsproContext.Update(login);
          await _vsproContext.SaveChangesAsync();
        }catch(DbUpdateConcurrencyException e)
        {
          throw new DbConcurrencyException(e.Message);
        }
      }
    

}