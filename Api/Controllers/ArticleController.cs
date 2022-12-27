using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ILogger<ArticleController> logger)
        {
            _logger = logger;
        }
        
        // [HttpGet]
        // public IEnumerable<ArticleData> GetArticles()
        // {
        //     
        // }
        //
        // [HttpGet("{id}")]
        // public IActionResult GetById(int id)
        // {
        //     // Return a single item by id
        // }
        //
        // [HttpPost]
        // public IActionResult Create([FromBody] ArticleData article)
        // {
        //     // Create a new item and return it
        // }
        //
        // [HttpPut("{id}")]
        // public IActionResult Update(int id, [FromBody] ArticleData article)
        // {
        //     // Update an existing item and return it
        // }
        //
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     // Delete an item and return a success status
        // }
    }
}
