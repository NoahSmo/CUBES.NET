using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.ViewModels;

namespace Api.Services;

public interface IArticleOrderService
{
    Task<List<ArticleOrder?>> GetArticleOrders();
    Task<ArticleOrder?> GetId(int id);
    Task<ArticleOrder> CreateArticleOrder(ArticleOrder articleOrder);
    Task<ArticleOrder>? UpdateArticleOrder(int id, ArticleOrder articleOrder);
    Task<ArticleOrder>? DeleteArticleOrder(int id);
}