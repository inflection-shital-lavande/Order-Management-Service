using Order_Management.app.domain_types.dto;
using Order_Management.Auth;

namespace Order_Management.app.database.service
{
    public interface IAddressService
    {
        Task<addressResponseDTO> GetAddressByIdAsync(Guid id);
        Task<List<addressResponseDTO>>GetAll();
        
        Task<Response> CreateAddressAsync(addressCreateDTO addressDto);
        Task<Response> UpdateAddressAsync(Guid id, addressUpdateDTO addressDto);
        Task<Response> DeleteAddressAsync(Guid id);
        Task<List<addressSearchResultsDTO>> SearchAddressesAsync(addressSearchFilterDTO filter);
    }
}


