using Checkpoint2.Data;

namespace Checkpoint2.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
