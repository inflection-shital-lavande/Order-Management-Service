using AutoMapper;
using Order_Management.app.database.models.DTO;
using Order_Management.app.database.models;

namespace Order_Management.app.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
