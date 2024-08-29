using Checkpoint2.Data;
using Checkpoint2.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WildPay.Exceptions;

namespace Checkpoint2.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddArticleAsync(CartArticle newArticle)
        {
            try
            {
                CartArticle? existingArticle = await _context.CartArticles.FirstOrDefaultAsync(el => el.BookId == newArticle.BookId && el.ApplicationUserId == newArticle.ApplicationUserId);

                if (existingArticle is not null) existingArticle.Quantity += newArticle.Quantity;
                else
                {
                    await _context.CartArticles.AddAsync(newArticle);
                }
                await _context.SaveChangesAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task<CartArticle> GetArticleByIdAsync(int articleId)
        {
            try
            {
                CartArticle? article = await _context.CartArticles.FirstOrDefaultAsync(el => el.Id == articleId);
                if (article == null) throw new NullException();

                return article;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task<List<CartArticle>> GetCartArticlesByUserIdAsync(string userId)
        {
            try
            {
                List<CartArticle> cartArticles = await _context.CartArticles.Where(el => el.ApplicationUserId == userId).Include(el => el.Book).ToListAsync();
                return cartArticles;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task RemoveArticleAsync(CartArticle articleToDelete)
        {
            try
            {
                _context.CartArticles.Remove(articleToDelete);
                await _context.SaveChangesAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task RemoveArticleByUserId(string userId)
        {
            try
            {
                foreach (CartArticle article in _context.CartArticles)
                {
                    if (article.ApplicationUserId == userId)
                    {
                        _context.CartArticles.Remove(article);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task TransferArticlesByUserId(string userId)
        {
            try
            {
                foreach (CartArticle cartArticle in _context.CartArticles)
                {
                    if (cartArticle.ApplicationUserId == userId)
                    {
                        BuyedArticle buyedArticle = new BuyedArticle
                        {
                            Id = cartArticle.Id,
                            ApplicationUserId = cartArticle.ApplicationUserId,
                            BookId = cartArticle.BookId,
                            Quantity = cartArticle.Quantity,
                        };
                        _context.BuyedArticles.AddAsync(buyedArticle);
                        _context.CartArticles.Remove(cartArticle);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task UpdateQuantityArticleAsync(int articleId, int quantity)
        {
            try
            {
                CartArticle articleToUpdate = await GetArticleByIdAsync(articleId);
                articleToUpdate.Quantity = quantity;

                await _context.SaveChangesAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }
    }
}
