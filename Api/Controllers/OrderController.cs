using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Npgsql;
using Api.Models;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Order\"", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                Id = reader.GetInt32(0),
                                Id_Client = reader.GetInt32(1),
                                Date = reader.GetDateTime(2),
                                Status = reader.GetString(3),
                                Serial = reader.GetString(4),
                            });
                        }
                    }
                }
            }
            return orders;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = new Order();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Order\" WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            order.Id = reader.GetInt32(0);
                            order.Id_Client = reader.GetInt32(1);
                            order.Date = reader.GetDateTime(2);
                            order.Status = reader.GetString(3);
                            order.Serial = reader.GetString(4);
                        }
                    }
                }
            }
            return Ok(order);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Order order)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Order\" (id_client, date, status, serial) VALUES (@id_client, @date, @status, @serial)", conn))
                {
                    cmd.Parameters.AddWithValue("id_client", order.Id_Client);
                    cmd.Parameters.AddWithValue("date", DateTime.Now);
                    cmd.Parameters.AddWithValue("status", order.Status);
                    cmd.Parameters.AddWithValue("serial", order.Serial);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Order order)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE public.\"Order\" SET id_client = @id_client, date = @date, status = @status, serial = @serial WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("id_client", order.Id_Client);
                    cmd.Parameters.AddWithValue("date", DateTime.Now);
                    cmd.Parameters.AddWithValue("status", order.Status);
                    cmd.Parameters.AddWithValue("serial", order.Serial);
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
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Order\" WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }
    }
}
