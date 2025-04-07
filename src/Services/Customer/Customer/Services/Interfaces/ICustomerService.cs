using Contract.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomerByUsernameAsync(string username);
    }
}
