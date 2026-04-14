using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _dbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
