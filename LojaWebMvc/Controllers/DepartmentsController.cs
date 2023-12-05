using LojaWebMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaWebMvc.Controllers;

public class DepartmentsController: Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var obj = await _departmentService.FindAllAsync();
        return View(obj);
    }
}