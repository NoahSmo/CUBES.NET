using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class StatusService : IStatusService
{
    
    private readonly DataContext _context;

    public StatusService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<StatusViewModel?>> GetStatuses()
    {
        return await _context.Statuses.Select(s => new StatusViewModel(s)).ToListAsync();
    }

    public async Task<StatusViewModel?> GetId(int id)
    {
        var status = await _context.Statuses.FindAsync(id);
        if (status is null)
            return null;

        return new StatusViewModel(status);
    }
    
    public async Task<StatusViewModel> CreateStatus(Status status)
    {
        _context.Statuses.Add(status);
        await _context.SaveChangesAsync();
        return new StatusViewModel(status);
    }
    
    public async Task<StatusViewModel>? UpdateStatus(int id, Status status)
    {
        var statusToUpdate = await _context.Statuses.FindAsync(id);
        if (statusToUpdate is null)
            return null;

        statusToUpdate.Message = status.Message;
        
        _context.Statuses.Update(statusToUpdate);
        await _context.SaveChangesAsync();

        return new StatusViewModel(statusToUpdate);
    }

    public async Task<StatusViewModel>? DeleteStatus(int id)
    {
        var status = await _context.Statuses.FindAsync(id);
        if (status is null)
            return null;

        _context.Statuses.Remove(status);
        await _context.SaveChangesAsync();

        return new StatusViewModel(status);
    }

    
}