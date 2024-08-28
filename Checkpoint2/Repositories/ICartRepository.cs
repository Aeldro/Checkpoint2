﻿using Checkpoint2.Models.Entities;

namespace Checkpoint2.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartArticle>> GetCartArticlesByUserIdAsync(string userId);
        Task<CartArticle> GetArticleByIdAsync(int articleId);
        Task AddArticleAsync(CartArticle newArticle);
        Task RemoveArticleAsync(CartArticle articleToDelete);
    }
}
