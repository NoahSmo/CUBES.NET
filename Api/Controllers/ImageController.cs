using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Image>>> GetImages()
        {
            return await _imageService.GetImages();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var result = await _imageService.GetId(id);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Image>> CreateImage(Image image)
        {
            var result = await _imageService.CreateImage(image);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Image>> UpdateImage(int id, Image image)
        {
            var result = await _imageService.UpdateImage(id, image);
            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Image>> DeleteImage(int id)
        {
            var result = await _imageService.DeleteImage(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
}