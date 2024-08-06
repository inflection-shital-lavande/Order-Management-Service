using AutoMapper;

using Order_Management.Auth;
using Order_Management.database.dto;
using Order_Management.database.models;

namespace Order_Management.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressCreateDTO>().ReverseMap();
            CreateMap<Address, AddressUpdateDTO>().ReverseMap();
            CreateMap<Address, AddressResponseDTO>().ReverseMap();
            CreateMap<Address, AddressSearchFilterDTO>().ReverseMap();
            CreateMap<Address, AddressSearchResultsDTO>().ReverseMap();

            CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();
            CreateMap<Customer, CustomerResponseDTO>().ReverseMap();
            CreateMap<Customer, CustomerSearchFilterDTO>().ReverseMap();
            CreateMap<Customer, CustomerSearchResultsDTO>().ReverseMap();

            CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
            CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();
            CreateMap<Coupon, CouponResponseDTO>().ReverseMap();
            CreateMap<Coupon, CouponSearchFilterDTO>().ReverseMap();
            CreateMap<Coupon, CouponSearchResultsDTO>().ReverseMap();


            CreateMap<CustomerAddress, CustomerAddressCreateDTO>().ReverseMap();

            //authentication
            CreateMap<RegisterDTO, User>();
        }
    }
}
