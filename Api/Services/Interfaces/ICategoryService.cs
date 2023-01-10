using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface ICategoryService
{
    Task<List<Category>> GetCategories();
    Task<Category?> GetId(int id);
    Task<List<Category>> CreateCategory(Category user);
    Task<List<Category>?> UpdateCategory(int id, Category user);
    Task<List<Category>?> DeleteCategory(int id);
}