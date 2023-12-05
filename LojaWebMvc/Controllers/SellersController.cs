using System.Diagnostics;
using LojaWebMvc.Data;
using LojaWebMvc.Models;
using LojaWebMvc.Models.ViewModels;
using LojaWebMvc.Services.Exceptions;
using LojaWebMvc.Services.Interfaces;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace LojaWebMvc.Controllers;

public class SellersController: Controller
{
    private readonly ISellerService _sellerService;
    private readonly IDepartmentService _departmentService;
    public SellersController(ISellerService sellerService, IDepartmentService departmentService)
    {
        _departmentService = departmentService;
        _sellerService = sellerService;
        
    }
    public async Task<IActionResult> Index()
    {
        var obj = await _sellerService.FindAllAsync();
        return View(obj);
    }
 
    public async Task<IActionResult> Details(int? id)
    {
        if(id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided!"});
        }
        var list = await _sellerService.FindByIdAsync(id.Value);
        if(list == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found!"});
        }
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var departments = await _departmentService.FindAllAsync();
        var viewModel = new SellerFormViewModel{Departments = departments};
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
        if(!ModelState.IsValid)
        {
           var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
            System.Console.WriteLine(ModelState.Values);
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel{ Seller = seller, Departments = departments};
            System.Console.WriteLine(message);
            return View("Create",viewModel);
        }
        await  _sellerService.InsertAsync(seller);
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null){
            return NotFound();
        }
        var obj = await _sellerService.FindByIdAsync(id.Value);
        if(obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided!"});
        }
        return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try{
        await _sellerService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
        }
        catch(IntegrityException e)
        {
            return RedirectToAction(nameof(Error), new {message = e.Message});
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if(id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided!"});
        }
        var obj = await _sellerService.FindByIdAsync(id.Value);
        if(obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided!"});
        }
        List<Department> departments = await _departmentService.FindAllAsync();
        SellerFormViewModel viewModel = new SellerFormViewModel{Seller = obj, Departments = departments};
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller)
    {
         if(!ModelState.IsValid)
        {
            List<Department> departments =  await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel{ Seller = seller, Departments = departments};
            return View("Views/Sellers/Edit.cshtml",viewModel);
        }
        if(id != seller.Id)
        {
           return RedirectToAction(nameof(Error), new { message = "Id mismatch!"});
        }
        try{
        await _sellerService.UpdateAsync(seller);
        return RedirectToAction(nameof(Index));
        }
        catch(ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message});
        }
        
    }
    public async Task<IActionResult> Error(string message)
    {
        var viewModel = new ErrorViewModel{
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
    }
    
}