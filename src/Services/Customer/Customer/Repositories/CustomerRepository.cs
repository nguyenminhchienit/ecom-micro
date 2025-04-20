using Contract.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryQueryBase<Entities.Customer, int, CustomerContext>, ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(CustomerContext dbContext, IServiceProvider services) : base(dbContext)
        {
            _logger = services.GetRequiredService<ILogger<CustomerRepository>>();
        }

        public async Task<List<Entities.Customer>> GetAllCustomer()
        {
            _logger.LogInformation("Get all customers");
            return await FindAll().ToListAsync();
        }

        public async Task<Entities.Customer> GetCustomerByUsernameAsync(string username) =>
            await FindByCondition(x => x.UserName.Equals(username))
            .SingleOrDefaultAsync();
    }
}
