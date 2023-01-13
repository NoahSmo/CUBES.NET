using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IArticleService
{
    Task<List<Article>> GetArticles();
    Task<Article?> GetId(int id);
    Task<Article> CreateArticle(Article article);
    Task<Article>? UpdateArticle(int id, Article article);
    Task<Article?> UpdateStock(Article article);
    Task<Article>? DeleteArticle(int id);
}