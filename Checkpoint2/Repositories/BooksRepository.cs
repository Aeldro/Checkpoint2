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

        async Task<List<Book>> IBooksRepository.GetAllBooksAsync()
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
