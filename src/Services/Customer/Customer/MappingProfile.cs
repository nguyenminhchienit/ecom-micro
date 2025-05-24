using AutoMapper;
using Shared.DTOs.Customers;

namespace Customer.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Customer, CustomerDto>();
        }
    }
}
