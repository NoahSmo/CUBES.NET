using System;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private string connectionString = "Server=localhost;Database=NEGOSUD;Username=postgres;Password=root";

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        
        
        
        [HttpGet]
        public async Task<IEnumerable<UserDetails>> Get()
        {
            return _userService.GetUsers();
        }
        
        [HttpGet("{id}")]
        public async Task<UserDetails> Get(int id)
        {
            return _userService.GetId(id);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] UserDetails user)
        {
            return _userService.CreateUser(user);
        }     
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDetails user)
        {
            return _userService.UpdateUser(id, user);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return _userService.DeleteUser(id);
        }
        

        [HttpPost("login")]
        public IActionResult Login([FromBody] User userData)
        {
            var password = userData.Password;
            var saltedPassword = password + salt;

            var hashedPasswordFromDb = "";


            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT password FROM public.\"User\" WHERE email = @email", conn))
                {
                    cmd.Parameters.AddWithValue("username", userData.Email);
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


        // [HttpGet]        
        // [Authorize]
        // public IEnumerable<UserDetails> GetUsers()
        // {
        //     var userDetailsData = new List<UserDetails>();
        //     using (var conn = new NpgsqlConnection(connectionString))
        //     {
        //         conn.Open();
        //         using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"User\"", conn))
        //         {
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     userDetailsData.Add(new UserDetails
        //                     {
        //                         Id = reader.GetInt32(0),
        //                         Username = reader.GetString(1),
        //                         Name = reader.IsDBNull(2) ? null : reader.GetString(2),
        //                         Surname = reader.IsDBNull(3) ? null : reader.GetString(3),
        //                         Email = reader.IsDBNull(4) ? null : reader.GetString(4),
        //                         Phone = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
        //                         Address = reader.IsDBNull(6) ? null : reader.GetString(6),
        //                         City = reader.IsDBNull(7) ? null : reader.GetString(7),
        //                         Country = reader.IsDBNull(8) ? null : reader.GetString(8),
        //                         PostCode = reader.IsDBNull(9) ? null : reader.GetString(9),
        //                         Password = reader.GetString(10),
        //                         Role = reader.IsDBNull(9) ? null : reader.GetString(11)
        //                     });
        //                 }
        //             }
        //         }
        //     }
        //
        //     return userDetailsData;
        // }
        //
        // [HttpGet("{id}")]
        // [Authorize]
        // public UserDetails GetById(int id)
        // {
        //     UserDetails user = null;
        //     using (var conn = new NpgsqlConnection(connectionString))
        //     {
        //         conn.Open();
        //         using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"User\" WHERE \"id\" = @id", conn))
        //         {
        //             cmd.Parameters.AddWithValue("id", id);
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     user = new UserDetails
        //                     {
        //                         Id = reader.GetInt32(0),
        //                         Username = reader.GetString(1),
        //                         Name = reader.IsDBNull(2) ? null : reader.GetString(2),
        //                         Surname = reader.IsDBNull(3) ? null : reader.GetString(3),
        //                         Email = reader.IsDBNull(4) ? null : reader.GetString(4),
        //                         Phone = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
        //                         Address = reader.IsDBNull(6) ? null : reader.GetString(6),
        //                         City = reader.IsDBNull(7) ? null : reader.GetString(7),
        //                         Country = reader.IsDBNull(8) ? null : reader.GetString(8),
        //                         PostCode = reader.IsDBNull(9) ? null : reader.GetString(9),
        //                         Password = reader.GetString(10),
        //                         Role = reader.IsDBNull(9) ? null : reader.GetString(11)
        //                     };
        //                 }
        //             }
        //         }
        //     }
        //
        //     return user;
        // }
        //
        // [HttpPost]
        // [Authorize]
        // public IActionResult Create([FromBody] UserDetails user)
        // {
        //     var password = user.Password;
        //     var saltedPassword = password + salt;
        //     var hashedPassword = BCrypt.Net.BCrypt.HashPassword(saltedPassword);
        //
        //     using (var conn = new NpgsqlConnection(connectionString))
        //     {
        //         conn.Open();
        //         using (var cmd = new NpgsqlCommand(
        //                    "INSERT INTO public.\"User\" (\"username\", \"name\", \"surname\", \"email\", \"phone\", \"address\", \"city\", \"country\", \"postCode\", \"password\", \"role\") VALUES (@username, @name, @surname, @email, @phone, @address, @city, @country, @postCode, @password, @role)",
        //                    conn))
        //         {
        //             cmd.Parameters.AddWithValue("username", user.Username);
        //             cmd.Parameters.AddWithValue("name", user.Name);
        //             cmd.Parameters.AddWithValue("surname", user.Surname);
        //             cmd.Parameters.AddWithValue("email", user.Email);
        //             cmd.Parameters.AddWithValue("phone", user.Phone);
        //             cmd.Parameters.AddWithValue("address", user.Address);
        //             cmd.Parameters.AddWithValue("city", user.City);
        //             cmd.Parameters.AddWithValue("country", user.Country);
        //             cmd.Parameters.AddWithValue("postCode", user.PostCode);
        //             cmd.Parameters.AddWithValue("password", hashedPassword);
        //             cmd.Parameters.AddWithValue("role", user.Role);
        //             cmd.ExecuteNonQuery();
        //         }
        //     }
        //
        //     return Ok();
        // }
        //
        // [HttpPut("{id}")]
        // [Authorize]
        // public IActionResult Update(int id, [FromBody] UserDetails user)
        // {
        //     var password = user.Password;
        //     var saltedPassword = password + salt;
        //     var hashedPassword = BCrypt.Net.BCrypt.HashPassword(saltedPassword);
        //
        //     using (var conn = new NpgsqlConnection(connectionString))
        //     {
        //         conn.Open();
        //         using (var cmd = new NpgsqlCommand(
        //                    "UPDATE public.\"User\" SET \"username\" = @username, \"name\" = @name, \"surname\" = @surname, \"email\" = @email, \"phone\" = @phone, \"address\" = @address, \"city\" = @city, \"country\" = @country, \"postCode\" = @postCode, \"password\" = @password, \"role\" = @role WHERE \"id\" = @id",
        //                    conn))
        //         {
        //             cmd.Parameters.AddWithValue("id", id);
        //             cmd.Parameters.AddWithValue("username", user.Username);
        //             cmd.Parameters.AddWithValue("name", user.Name);
        //             cmd.Parameters.AddWithValue("surname", user.Surname);
        //             cmd.Parameters.AddWithValue("email", user.Email);
        //             cmd.Parameters.AddWithValue("phone", user.Phone);
        //             cmd.Parameters.AddWithValue("address", user.Address);
        //             cmd.Parameters.AddWithValue("city", user.City);
        //             cmd.Parameters.AddWithValue("country", user.Country);
        //             cmd.Parameters.AddWithValue("postCode", user.PostCode);
        //             cmd.Parameters.AddWithValue("password", hashedPassword);
        //             cmd.Parameters.AddWithValue("role", user.Role);
        //             cmd.ExecuteNonQuery();
        //         }
        //     }
        //
        //     return Ok();
        // }
        //
        // [HttpDelete("{id}")]
        // [Authorize]
        // public IActionResult Delete(int id)
        // {
        //     using (var conn = new NpgsqlConnection(connectionString))
        //     {
        //         conn.Open();
        //         using (var cmd = new NpgsqlCommand("DELETE FROM public.\"User\" WHERE \"id\" = @id", conn))
        //         {
        //             cmd.Parameters.AddWithValue("id", id);
        //             cmd.ExecuteNonQuery();
        //         }
        //     }
        //
        //     return Ok();
        // }
    }
}