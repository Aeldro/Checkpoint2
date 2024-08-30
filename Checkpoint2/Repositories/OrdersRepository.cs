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

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                List<Order> userOrders = await _context.Orders
                .Where(el => el.ApplicationUserId == userId)
                .Include(el => el.Articles)
                .ThenInclude(article => article.Book)
                .Include(el => el.Articles)
                .ThenInclude(article => article.Payer)
                .ToListAsync();

                return userOrders;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseException();
            }
        }
    }
}
