﻿using Contract.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order, long>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var result = FindByCondition(x => x.UserName.Equals(userName));
            return await result.ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await CreateAsync(order);
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await UpdateAsync(order);
            return order;
        }
    }
}
