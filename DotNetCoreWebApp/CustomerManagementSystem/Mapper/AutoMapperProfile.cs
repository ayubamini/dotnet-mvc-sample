using AutoMapper;
using CustomerManagementSystem.DATA.Entities;
using CustomerManagementSystem.Models.CustomerViewModel;

namespace CustomerManagementSystem.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerVM>().ReverseMap();
        }
    }
}