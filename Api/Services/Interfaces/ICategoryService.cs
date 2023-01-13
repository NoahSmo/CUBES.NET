using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface ICategoryService
{
    Task<List<Category>> GetCategories();
    Task<Category?> GetId(int id);
    Task<Category> CreateCategory(Category user);
    Task<Category>? UpdateCategory(int id, Category user);
    Task<Category>? DeleteCategory(int id);
}