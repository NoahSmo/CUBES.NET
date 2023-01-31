using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IStatusService
{
    Task<List<StatusViewModel?>> GetStatuses();
    Task<StatusViewModel?> GetId(int id);
    Task<StatusViewModel> CreateStatus(Status status);
    Task<StatusViewModel>? UpdateStatus(int id, Status status);
    Task<StatusViewModel>? DeleteStatus(int id);
}