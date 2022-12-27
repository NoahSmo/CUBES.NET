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
    public class CategoryController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CategoryData> GetCategories()
        {
            var categoryData = new List<CategoryData>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Category\"", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryData.Add(new CategoryData
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                            });
                        }
                    }
                }
            }

            return categoryData;
        }

        [HttpGet("{id}")]
        public CategoryData GetById(int id)
        {
            CategoryData category = null;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( "SELECT * FROM public.\"Category\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            category = new CategoryData
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                            };
                        }
                    }
                }
            }

            return category;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryData category)
        {         
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Category\" (\"name\", \"description\") VALUES (@name, @description)", conn))
                {
                    cmd.Parameters.AddWithValue("name", category.Name);
                    cmd.Parameters.AddWithValue("description", category.Description);
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryData category)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE public.\"Category\" SET \"name\" = @name, \"description\" = @description WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", category.Name);
                    cmd.Parameters.AddWithValue("description", category.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Category\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            
            return Ok();
        }
    }
}