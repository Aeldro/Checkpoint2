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
                //bool doesArticleExists = _context.CartArticles.Any(el => el.BookId == newArticle.BookId && el.UserId == newArticle.UserId);
                CartArticle? existingArticle = await _context.CartArticles.FirstOrDefaultAsync(el => el.BookId == newArticle.BookId && el.UserId == newArticle.UserId);

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
                List<CartArticle> cartArticles = await _context.CartArticles.Where(el => el.UserId == userId).Include(el => el.Book).ToListAsync();
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
    }
}
