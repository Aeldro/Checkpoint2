using Checkpoint2.Models.Entities;

namespace Checkpoint2.Repositories
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetBooksAsync(string searchedString);
        Task<Book> GetBookByIdAsync(int bookId);

    }
}
