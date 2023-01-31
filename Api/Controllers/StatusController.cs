﻿using Api.Models;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<StatusViewModel>>> GetStatus()
        {
            return await _statusService.GetStatuses();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusViewModel>> GetStatus(int id)
        {
            var result = await _statusService.GetId(id);
            return result == null ? NotFound("Status not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<StatusViewModel>> CreateStatus(Status status)
        {
            var result = await _statusService.CreateStatus(status);
            return result == null ? Unauthorized("Status already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<StatusViewModel>> UpdateStatus(int id, Status status)
        {
            var result = await _statusService.UpdateStatus(id, status);
            return result == null ? NotFound("Status not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<StatusViewModel>> DeleteStatus(int id)
        {
            var result = await _statusService.DeleteStatus(id);
            return result == null ? NotFound("Status not found") : Ok(result);
        }
    }
}