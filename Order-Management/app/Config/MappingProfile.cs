using AutoMapper;
using Order_Management.app.database.models;
using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.cutomerModelDTO;
using Order_Management.Auth;
namespace Order_Management.app.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, addressCreateDTO>().ReverseMap();
            CreateMap<Address, addressUpdateDTO>().ReverseMap();
            CreateMap<Address, addressResponseDTO>().ReverseMap();
            CreateMap<Address, addressSearchFilterDTO>().ReverseMap();
            CreateMap<Address, addressSearchResultsDTO>().ReverseMap();

            CreateMap<Customer, customerCreateDTO>().ReverseMap();
            CreateMap<Customer, customerUpdateDTO>().ReverseMap();
            CreateMap<Customer, customerResponseDTO>().ReverseMap();
            CreateMap<Customer, customerSearchFilterDTO>().ReverseMap();
            CreateMap<Customer, customerSearchResultsDTO>().ReverseMap();

            CreateMap<CustomerAddress, customerAddressCreateDTO>().ReverseMap();

            //authentication
            CreateMap<RegisterDTO, User>();
        }
    }
}
