using System.Diagnostics;
using LojaWebMvc.Models;
using LojaWebMvc.Models.ViewModels;
using LojaWebMvc.Services.Exceptions;
using LojaWebMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Org.BouncyCastle.Security;

namespace LojaWebMvc.Controllers;

public class ServersController : Controller
{
    private readonly IServerService _serverService;


    public ServersController(IServerService serverService)
    {
        _serverService = serverService;
    }

    public async Task<IActionResult> Index()
    {
        var obj = await _serverService.FindAllAsync();
        return View(obj);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Server server)
    {
         await _serverService.InsertAsync(server);
        
         return RedirectToAction("Index");

    }

    public async Task<IActionResult> Details(int id)
    {
         var list = await _serverService.FindAsync(id);
        if(id == null)
        {
            return BadRequest();
        }
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not mismatch!"});
        }
        var obj = await _serverService.FindAsync(id.Value);
        if(obj == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id not provided!"});
        }
        return View(obj);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try{
            
            await _serverService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));

        }catch(IntegrityException e){
            return RedirectToAction(nameof(Error), new {message = e.Message});
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
       if(id == null)
       {
            return RedirectToAction(nameof(Error), new {message = "Id not provided"});
       }
       var obj = await _serverService.FindAsync(id.Value);
       if(obj == null)
       {
        return RedirectToAction(nameof(Error), new {message = "Id not provided"});
       }
       var server = await _serverService.FindAsync(id.Value);
       return View(obj);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Server server)
    {
        if(!ModelState.IsValid)
        {
            var obj = await _serverService.FindAsync(id);
            return View(obj);
        }
        if(id != server.Id)
        {
            return RedirectToAction(nameof(Error), new {message = "Id mismatch ! "});
        }
        try
        {
            await _serverService.UpdateAsync(server);
            return RedirectToAction(nameof(Index));

        }catch(ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new{ message = e.Message});
        }
    }
    public async Task<IActionResult> Error(string message)
    {
        var ViewModel = new ErrorViewModel{
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(ViewModel);
    }

}