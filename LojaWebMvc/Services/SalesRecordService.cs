using LojaWebMvc.Models;
using LojaWebMvc.Services.Interfaces;
using LojaWebMvc.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LojaWebMvc.Services.Exceptions;

namespace LojaWebMvc.Services;

public class SalesRecordService: ISalesRecordService
{
    private readonly VsproContext _vsproContext;
    public SalesRecordService(VsproContext vsproContext)
    {
        _vsproContext = vsproContext;
    }

    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from ojb in _vsproContext.SalesRecord select ojb;
       if(minDate.HasValue)
       {
            result = result.Where(x => x.Date >= minDate.Value);
       }
       if(maxDate.HasValue)
       {
        result = result.Where(x => x.Date >= maxDate.Value);
       }
       return await result.Include(x => x.Seller)
                    .Include(x=> x.Seller.Department)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync();
    }
}