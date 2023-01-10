using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Provider> GetProviders()
        {
            var providerData = new List<Provider>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Provider\"", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            providerData.Add(new Provider
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.GetString(3),
                            });
                        }
                    }
                }
            }

            return providerData;
        }

        [HttpGet("{id}")]
        public Provider GetById(int id)
        {
            Provider client = null;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( "SELECT * FROM public.\"Provider\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            client = new Provider
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.GetString(3),
                            };
                        }
                    }
                }
            }

            return client;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Provider client)
        {         
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Provider\" (\"name\", \"email\", \"phone\") VALUES (@name, @email, @phone)", conn))
                {
                    cmd.Parameters.AddWithValue("name", client.Name);                    
                    cmd.Parameters.AddWithValue("email", client.Email);
                    cmd.Parameters.AddWithValue("phone", client.Phone);
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Provider client)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE public.\"Provider\" SET \"name\" = @name, \"email\" = @email WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", client.Name);                    
                    cmd.Parameters.AddWithValue("email", client.Email);
                    cmd.Parameters.AddWithValue("phone", client.Phone);
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
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Provider\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            
            return Ok();
        }
    }
}