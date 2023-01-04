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
        
        [HttpGet]
        public IEnumerable<ArticleData> GetArticles()
        {
            var articles = new List<ArticleData>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Article\"", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articles.Add(new ArticleData
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Image = reader.GetString(3),
                                Year = reader.GetString(4),
                                Price = reader.GetString(5),
                                Id_Provider = reader.GetInt32(6),
                                Id_Category = reader.GetInt32(7),
                                Stock = reader.GetInt32(8)
                            });
                        }
                    }
                }
            }
            return articles;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var article = new ArticleData();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.\"Article\" WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            article.Id = reader.GetInt32(0);
                            article.Name = reader.GetString(1);
                            article.Description = reader.GetString(2);
                            article.Image = reader.GetString(3);
                            article.Year = reader.GetString(4);
                            article.Price = reader.GetString(5);
                            article.Id_Provider = reader.GetInt32(6);
                            article.Id_Category = reader.GetInt32(7);
                            article.Stock = reader.GetInt32(8);
                        }
                    }
                }
            }
            return Ok(article);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] ArticleData article)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO public.\"Article\" (name, description, image, year, price, id_provider, id_category, stock) VALUES (@name, @description, @image, @year, @price, @id_provider, @id_category, @stock)", conn))
                {
                    cmd.Parameters.AddWithValue("name", article.Name);
                    cmd.Parameters.AddWithValue("description", article.Description);
                    cmd.Parameters.AddWithValue("image", article.Image);
                    cmd.Parameters.AddWithValue("year", article.Year);
                    cmd.Parameters.AddWithValue("price", article.Price);
                    cmd.Parameters.AddWithValue("id_provider", article.Id_Provider);
                    cmd.Parameters.AddWithValue("id_category", article.Id_Category);
                    cmd.Parameters.AddWithValue("stock", article.Stock);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ArticleData article)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE public.\"Article\" SET name = @name, description = @description, image = @image, year = @year, price = @price, id_provider = @id_provider, id_category = @id_category, stock = @stock WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", article.Name);
                    cmd.Parameters.AddWithValue("description", article.Description);
                    cmd.Parameters.AddWithValue("image", article.Image);
                    cmd.Parameters.AddWithValue("year", article.Year);
                    cmd.Parameters.AddWithValue("price", article.Price);
                    cmd.Parameters.AddWithValue("id_provider", article.Id_Provider);
                    cmd.Parameters.AddWithValue("id_category", article.Id_Category);
                    cmd.Parameters.AddWithValue("stock", article.Stock);
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
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Article\" WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }
    }
}
