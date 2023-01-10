using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class CategoryService : ICategoryService
{
    
    private readonly DataContext _context;

    public CategoryService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Category>> GetCategories()
    {
        var categorys = await _context.Categories.ToListAsync();
        return categorys;
    }

    public async Task<Category?> GetId(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return null;

        return category;
    }
    
    public async Task<List<Category>> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return await _context.Categories.ToListAsync();
    }
    
    public async Task<List<Category>?> UpdateCategory(int id, Category request)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return null;

        category.Name = request.Name;

        await _context.SaveChangesAsync();

        return await _context.Categories.ToListAsync();
    }

    public async Task<List<Category>?> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return null;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return await _context.Categories.ToListAsync();
    }

    
}