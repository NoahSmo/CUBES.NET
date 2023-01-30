using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IStatusService
{
    Task<List<Status?>> GetStatuses();
    Task<Status?> GetId(int id);
    Task<Status> CreateStatus(Status status);
    Task<Status>? UpdateStatus(int id, Status status);
    Task<Status>? DeleteStatus(int id);
}