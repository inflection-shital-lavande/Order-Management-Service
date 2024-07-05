using Order_Management.app.domain_types.dto;

namespace Order_Management.app.database.service
{
    public interface IAddressService
    {
        Task<addressResponseDTO> GetAddressByIdAsync(Guid id);
        Task<IEnumerable<addressResponseDTO>> GetAllAddressesAsync();
        Task<IEnumerable<addressResponseDTO>> SearchAddressesAsync(addressSearchFilterDTO filter);
        Task<addressResponseDTO> CreateAddressAsync(addressCreateDTO addressDto);
        Task<addressResponseDTO> UpdateAddressAsync(Guid id, addressUpdateDTO addressDto);
        Task<bool> DeleteAddressAsync(Guid id);
    }
}


