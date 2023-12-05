using LojaWebMvc.Data;
using LojaWebMvc.Models;
using LojaWebMvc.Models.ViewModels;
using LojaWebMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LojaWebMvc.Services.Exceptions;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LojaWebMvc.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LojaWebMvc.Models;
using LojaWebMvc.Models.ViewModels;
using LojaWebMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LojaWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace LojaWebMvc.Services;

public class ServerService: IServerService
{
     private readonly VsproContext _vsproContext;

    public ServerService(VsproContext vsproContext)
    {
        _vsproContext = vsproContext;
    }

    public async Task<List<Server>> FindAllAsync()
    {
        var list = _vsproContext.Server;
        var obj = new List<Server>();
        obj = await list.Select(x => new Server(
            x.Id,
            x.Name,
            x.Address,
            x.Status
            
        )).ToListAsync();
        return obj;
    }

    public async Task<Server> FindAsync(int id)
    {
         return await _vsproContext.Server.FindAsync(id);

    }

    public async Task InsertAsync(Server server)
    {
        _vsproContext.Add(server);
        await _vsproContext.SaveChangesAsync();
    }


    public async Task RemoveAsync (int id)
    {
       try{
            var obj = await _vsproContext.Server.FindAsync(id);
            _vsproContext.Remove(obj);
            await _vsproContext.SaveChangesAsync();
        }catch(DbUpdateException e)
        {
         throw new IntegrityException(e.Message);
        }
    }

    public async Task UpdateAsync(Server server)
    {
        bool hasAny = await _vsproContext.Server.AnyAsync(x => x.Id == server.Id);
        if(!hasAny)
        {
            throw new NotFoundException("Error - Id mismatch");

        }
        try{
            _vsproContext.Update(server);
            await _vsproContext.SaveChangesAsync();
        }catch(DbUpdateConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }

    }
}