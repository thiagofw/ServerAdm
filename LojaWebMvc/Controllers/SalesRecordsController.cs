using LojaWebMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaWebMvc.Controllers;

public class SalesRecordsController: Controller
{
    private readonly ISalesRecordService _salesRecordService;

    public SalesRecordsController(ISalesRecordService salesRecordService)
    {
        _salesRecordService = salesRecordService;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
    {
        var obj = await _salesRecordService.FindByDateAsync(minDate, maxDate);
        return View(obj);
    }

    public IActionResult GroupingSearch()
    {
        return View();
    }

}