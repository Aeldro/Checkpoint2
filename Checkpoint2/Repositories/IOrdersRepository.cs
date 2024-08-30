using Checkpoint2.Models.Entities;

namespace Checkpoint2.Repositories
{
    public interface IOrdersRepository
    {
        Task AddOrderAsync(Order newCommand);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
