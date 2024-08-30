using Checkpoint2.Data;
using Checkpoint2.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WildPay.Exceptions;

namespace Checkpoint2.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order newCommand)
        {
            try
            {
                await _context.Orders.AddAsync(newCommand);
                await _context.SaveChangesAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }
    }
}
