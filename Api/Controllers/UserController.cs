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
    public class UserController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        public string salt = "a849qsd4165x146wxc436s1dqs32d4azq98d74q6d1azsdqs6d5qs4dq5s1d3qs6d47q5sd6wx1cwxc";
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserData userData)
        {
            
            var password = userData.Password;
            var saltedPassword = password + salt;
            
            var hashedPasswordFromDb = "";


            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT password FROM public.\"User\" WHERE username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("username", userData.Username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hashedPasswordFromDb = reader.GetString(0);
                        }
                    }
                }
            }
            
            if (BCrypt.Net.BCrypt.Verify(saltedPassword, hashedPasswordFromDb))
            {
                return Ok("Logged in");
            }
            else
            {
                return BadRequest("Wrong password");
            }
            
        }
        
        

        [HttpGet]
        public IEnumerable<UserDetailsData> GetUsers()
        {
            var userDetailsData = new List<UserDetailsData>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"User\"", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userDetailsData.Add(new UserDetailsData
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Surname = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Phone = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                                Address = reader.IsDBNull(6) ? null : reader.GetString(6),
                                City = reader.IsDBNull(7) ? null : reader.GetString(7),
                                Country = reader.IsDBNull(8) ? null : reader.GetString(8),
                                PostCode = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Password = reader.GetString(10),
                                Role = reader.IsDBNull(9) ? null : reader.GetString(11)
                            });
                        }
                    }
                }
            }

            return userDetailsData;
        }

        [HttpGet("{id}")]
        public UserDetailsData GetById(int id)
        {
            UserDetailsData user = null;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand( "SELECT * FROM public.\"User\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new UserDetailsData
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Name = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Surname = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Phone = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                                Address = reader.IsDBNull(6) ? null : reader.GetString(6),
                                City = reader.IsDBNull(7) ? null : reader.GetString(7),
                                Country = reader.IsDBNull(8) ? null : reader.GetString(8),
                                PostCode = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Password = reader.GetString(10),
                                Role = reader.IsDBNull(9) ? null : reader.GetString(11)
                            };
                        }
                    }
                }
            }

            return user;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDetailsData user)
        {         
            
            var password = user.Password;
            var saltedPassword = password + salt;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(saltedPassword);
            
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"User\" (\"username\", \"name\", \"surname\", \"email\", \"phone\", \"address\", \"city\", \"country\", \"postCode\", \"password\", \"role\") VALUES (@username, @name, @surname, @email, @phone, @address, @city, @country, @postCode, @password, @role)", conn))
                {
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("name", user.Name);
                    cmd.Parameters.AddWithValue("surname", user.Surname);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("phone", user.Phone);
                    cmd.Parameters.AddWithValue("address", user.Address);
                    cmd.Parameters.AddWithValue("city", user.City);
                    cmd.Parameters.AddWithValue("country", user.Country);
                    cmd.Parameters.AddWithValue("postCode", user.PostCode);
                    cmd.Parameters.AddWithValue("password", hashedPassword);
                    cmd.Parameters.AddWithValue("role", user.Role);
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDetailsData user)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE public.\"User\" SET \"username\" = @username, \"name\" = @name, \"surname\" = @surname, \"email\" = @email, \"phone\" = @phone, \"address\" = @address, \"city\" = @city, \"country\" = @country, \"postCode\" = @postCode, \"password\" = @password, \"role\" = @role WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("name", user.Name);
                    cmd.Parameters.AddWithValue("surname", user.Surname);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("phone", user.Phone);
                    cmd.Parameters.AddWithValue("address", user.Address);
                    cmd.Parameters.AddWithValue("city", user.City);
                    cmd.Parameters.AddWithValue("country", user.Country);
                    cmd.Parameters.AddWithValue("postCode", user.PostCode);
                    cmd.Parameters.AddWithValue("password", user.Password);
                    cmd.Parameters.AddWithValue("role", user.Role);
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
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"User\" WHERE \"id\" = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            
            return Ok();
        }
    }
}