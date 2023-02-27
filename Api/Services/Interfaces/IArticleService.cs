using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IArticleService
{
    Task<List<Article>> GetArticles();
    Task<Article?> GetId(int id);
    Task<ArticleViewModel> CreateArticle(Article article);
    Task<ArticleViewModel>? UpdateArticle(int id, Article article);
    Task<Article?> UpdateStock(Article article);
    Task<ArticleViewModel>? DeleteArticle(int id);
}