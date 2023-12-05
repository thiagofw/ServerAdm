using LojaWebMvc.Models;
using LojaWebMvc.Services.Interfaces;
using LojaWebMvc.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LojaWebMvc.Services.Exceptions;

namespace LojaWebMvc.Services;

public class SellerService: ISellerService
{
    private readonly VsproContext _vsproContext;
    public SellerService(VsproContext vsproContext)
    {
        _vsproContext = vsproContext;
    }
//
        public Task<List<Seller>> FindAllAsync()
        {
           // return _vsproContext.Seller.OrderBy(x => x.Name).ToList();
            var list = _vsproContext.Seller;
            var obj = list.Select(x => new Seller(
                x.Id,
                x.Name,
                x.Email,
                x.BirthDate,
                x.BaseSalary,
                x.Department

            )).ToListAsync();
            return obj;
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _vsproContext.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id);
               // var obj = _vsproContext.Seller.Find(id);
                

        }

        public async Task InsertAsync(Seller seller)
        {
            _vsproContext.Add(seller);
           await _vsproContext.SaveChangesAsync();
            
        }

        public async Task RemoveAsync(int id)
        {
            try{
            var obj = await _vsproContext.Seller.FindAsync(id);
            _vsproContext.Remove(obj);
            await _vsproContext.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }

        }

        public async Task UpdateAsync(Seller seller)
       {
            bool hasAny = await _vsproContext.Seller.AnyAsync(x => x.Id == seller.Id);

            if(!hasAny)
            {
                throw new NotFoundException("Error - Id not found");
            }
            try{
            _vsproContext.Update(seller);
           await _vsproContext.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException e) 
            {
                throw new DbConcurrencyException(e.Message);
            }

       }


    
}