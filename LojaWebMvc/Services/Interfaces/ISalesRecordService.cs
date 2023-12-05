using LojaWebMvc.Data;
using LojaWebMvc.Models;

namespace LojaWebMvc.Services.Interfaces;

public interface ISalesRecordService
{

    Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
    

}