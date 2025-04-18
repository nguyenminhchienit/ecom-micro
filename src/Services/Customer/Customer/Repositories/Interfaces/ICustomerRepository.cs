﻿using Contract.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryQueryBase<Entities.Customer, int, CustomerContext>
    {
        Task<Entities.Customer> GetCustomerByUsernameAsync(string username);
        Task<List<Entities.Customer>> GetAllCustomer();
    }
}
