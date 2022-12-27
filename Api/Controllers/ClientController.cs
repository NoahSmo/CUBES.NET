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
    public class ClientController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ClientData> GetClients()
        {
            var clientData = new List<ClientData>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Client\"", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientData.Add(new ClientData
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                Phone = reader.GetInt32(3),
                                Email = reader.GetString(4),
                                Address = reader.GetString(5),
                                City = reader.GetString(6),
                                PostCode = reader.GetInt32(7),
                                Country = reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return clientData;
        }

        [HttpGet("{id}")]
        public ClientData GetById(int id)
        {
            ClientData client = null;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( "SELECT * FROM public.\"Client\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            client = new ClientData
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                Phone = reader.GetInt32(3),
                                Email = reader.GetString(4),
                                Address = reader.GetString(5),
                                City = reader.GetString(6),
                                PostCode = reader.GetInt32(7),
                                Country = reader.GetString(8)
                            };
                        }
                    }
                }
            }

            return client;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientData client)
        {         
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Client\" (\"name\", \"surname\", \"phone\", \"email\", \"address\", \"city\", \"postCode\", \"country\", \"password\") VALUES (@name, @surname, @phone, @email, @address, @city, @postCode, @country, @password)", conn))
                {
                    cmd.Parameters.AddWithValue("name", client.Name);
                    cmd.Parameters.AddWithValue("surname", client.Surname);
                    cmd.Parameters.AddWithValue("phone", client.Phone);
                    cmd.Parameters.AddWithValue("email", client.Email);
                    cmd.Parameters.AddWithValue("address", client.Address);
                    cmd.Parameters.AddWithValue("city", client.City);
                    cmd.Parameters.AddWithValue("postCode", client.PostCode);
                    cmd.Parameters.AddWithValue("country", client.Country);
                    cmd.Parameters.AddWithValue("password", client.Password);
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClientData client)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE public.\"Client\" SET \"name\" = @name, \"surname\" = @surname, \"phone\" = @phone, \"email\" = @email, \"address\" = @address, \"city\" = @city, \"postCode\" = @postCode, \"country\" = @country, \"password\" = @password WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", client.Name);
                    cmd.Parameters.AddWithValue("surname", client.Surname);
                    cmd.Parameters.AddWithValue("phone", client.Phone);
                    cmd.Parameters.AddWithValue("email", client.Email);
                    cmd.Parameters.AddWithValue("address", client.Address);
                    cmd.Parameters.AddWithValue("city", client.City);
                    cmd.Parameters.AddWithValue("postCode", client.PostCode);
                    cmd.Parameters.AddWithValue("country", client.Country);
                    cmd.Parameters.AddWithValue("password", client.Password);
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
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Client\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            
            return Ok();
        }
    }
}