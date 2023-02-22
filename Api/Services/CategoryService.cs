using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class CategoryService : ICategoryService
{
    
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Category>> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetId(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return null;

        return category;
    }
    
    public async Task<Category> CreateCategory(Category category)
    {
        category.Id = _context.Categories.Max(x => x.Id) + 1;  
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
    
    public async Task<Category>? UpdateCategory(int id, Category request)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return null;

        category.Name = request.Name;
        category.Description = request.Description;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category>? DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return null;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return category;
    }

    
}