using Checkpoint2.Data;
using Checkpoint2.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WildPay.Exceptions;

namespace Checkpoint2.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ApplicationDbContext _context;

        public BooksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                Book? book = await _context.Books.FindAsync(bookId);
                if (book == null) throw new NullException();
                return book;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task<List<Book>> GetBooksAsync(string searchedString)
        {
            try
            {
                List<Book> books = await _context.Books.Where(el => el.Title.Contains(searchedString)).Include(el => el.Author).Include(el => el.Category).ToListAsync();
                return books;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                List<Book> books = await _context.Books.Include(b => b.Author).Include(b => b.Category).ToListAsync();
                return books;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }
    }
}
