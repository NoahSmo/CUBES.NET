using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IArticleService
{
    Task<List<Article>> GetArticles();
    Task<Article?> GetId(int id);
    Task<List<Article>> CreateArticle(Article user);
    Task<List<Article>?> UpdateArticle(int id, Article user);
    Task<List<Article>?> DeleteArticle(int id);
}