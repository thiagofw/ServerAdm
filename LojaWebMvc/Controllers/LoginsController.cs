using LojaWebMvc.Models;
using LojaWebMvc.Models.ViewModels;
using LojaWebMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LojaWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace LojaWebMvc.Controllers;
public class LoginsController: Controller
{
    private readonly ILoginService _loginService;
    private readonly IServerService _serverService;

    public LoginsController(ILoginService loginService, IServerService serverService)
    {
        _loginService = loginService;
        _serverService = serverService;
    }

    public async Task<IActionResult> Index()
    {
        var obj = await _loginService.FindAllAsync();
        return View(obj);
    }
    public async Task<IActionResult> Details(int id)
    {
        var list = await _loginService.FindByIdAsync(id);
        if(id == null)
        {
            return BadRequest();
        }
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var servers = await _serverService.FindAllAsync();
        var viewModel = new LoginFormViewModel{Servers = servers};
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Login login)
    {
        if(!ModelState.IsValid){
        var servers = await _serverService.FindAllAsync();
        var viewModel = new LoginFormViewModel{Servers=servers, Login=login};
        return View("Create", viewModel);
        }
        await _loginService.InsertAsync(login);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)
        {
             return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }

        var obj = await _loginService.FindByIdAsync(id.Value);

        if(obj == null)
        {
             return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }
        return View(obj);

    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try{
            await _loginService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));

        }catch(IntegrityException e)
        {
             return RedirectToAction(nameof(Error), new {message = e.Message});
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if(id == null)
        {
             return RedirectToAction(nameof(Error), new { message = "Id is empty" });
        }

        var obj = await _loginService.FindByIdAsync(id.Value);
        
        if(obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }
        List<Server> servers = await _serverService.FindAllAsync();
        LoginFormViewModel formViewModel = new LoginFormViewModel{Servers=servers, Login=obj};
        return View(formViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Login login)
    {
        if(!ModelState.IsValid){
            List<Server> servers = await _serverService.FindAllAsync();
            LoginFormViewModel formViewModel = new LoginFormViewModel{ Servers = servers, Login = login };
            return View(formViewModel);
        }
        if(id != login.Id)
        {
              return RedirectToAction(nameof(Error), new { message = "Id mismatch!"});
        }
        try{
          
            await _loginService.UpdateAsync(login);
             return RedirectToAction(nameof(Index));
        }catch(ApplicationException e)
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