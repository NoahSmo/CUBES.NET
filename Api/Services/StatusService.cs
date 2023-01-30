using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class StatusService : IStatusService
{
    
    private readonly DataContext _context;

    public StatusService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Status?>> GetStatuses()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<Status?> GetId(int id)
    {
        var status = await _context.Statuses.FindAsync(id);
        if (status is null)
            return null;

        return status;
    }
    
    public async Task<Status> CreateStatus(Status status)
    {
        _context.Statuses.Add(status);
        await _context.SaveChangesAsync();
        return status;
    }
    
    public async Task<Status>? UpdateStatus(int id, Status status)
    {
        var statusToUpdate = await _context.Statuses.FindAsync(id);
        if (statusToUpdate is null)
            return null;

        statusToUpdate.Message = status.Message;
        
        _context.Statuses.Update(statusToUpdate);
        await _context.SaveChangesAsync();

        return statusToUpdate;
    }

    public async Task<Status>? DeleteStatus(int id)
    {
        var status = await _context.Statuses.FindAsync(id);
        if (status is null)
            return null;

        _context.Statuses.Remove(status);
        await _context.SaveChangesAsync();

        return status;
    }

    
}