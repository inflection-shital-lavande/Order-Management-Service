using Order_Management.Auth;
using order_management.common;
using Order_Management.database.dto;
using Order_Management.database.models;

namespace Order_Management.services.interfaces
{
    public interface IAddressService
    {
        Task<AddressResponseDTO> GetAddressById(Guid id);
        Task<List<AddressResponseDTO>> GetAll();

        Task<IResult> CreateAddress(AddressCreateDTO addressDto);
        Task<IResult> UpdateAddress(Guid id, AddressUpdateDTO addressDto);


        //  Task<Response> DeleteAddressAsync(Guid id);
       

        Task<IResult> DeleteAddress(Guid id);
        Task<List<AddressSearchResultsDTO>> SearchAddress(AddressSearchFilterDTO filter);

       // ICollection<Customer> GetCustomerByAddress(Guid id);
    }
}
