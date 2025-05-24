using AutoMapper;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Shared.DTOs.Customers;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResult> GetAllCustomerAsync()
        {
            return Results.Ok(await _repository.GetAllCustomer());
        }

        //public async Task<IResult> GetCustomerByUsernameAsync(string username) =>
        //    Results.Ok(await _repository.GetCustomerByUsernameAsync(username));

        public async Task<IResult> GetCustomerByUsernameAsync(string username)
        {
            var entity = await _repository.GetCustomerByUsernameAsync(username);
            var result = _mapper.Map<CustomerDto>(entity);

            return Results.Ok(result);
        }
    }
}
