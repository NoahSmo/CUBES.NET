using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategories();
    Task<Category?> GetId(int id);
    Task<Category> CreateCategory(Category user);
    Task<Category>? UpdateCategory(int id, Category user);
    Task<Category>? DeleteCategory(int id);
}