using Microsoft.EntityFrameworkCore;
using Order_Management.app.database.models;
using Order_Management.app.domain_types.dto;
using Order_Management.Data;

namespace Order_Management.app.database.service
{

    public class AddressService : IAddressService
    {
        private readonly OrderManagementContext _context;

        public AddressService(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<addressResponseDTO> GetAddressByIdAsync(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            return MapToAddressResponseDto(address);
        }

        public async Task<IEnumerable<addressResponseDTO>> GetAllAddressesAsync()
        {
            var addresses = await _context.Addresses.ToListAsync();
            return addresses.Select(a => MapToAddressResponseDto(a));
        }

        public async Task<IEnumerable<addressResponseDTO>> SearchAddressesAsync(addressSearchFilterDTO filter)
        {
            var query = _context.Addresses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.AddressLine1))
                query = query.Where(a => a.AddressLine1.Contains(filter.AddressLine1));

            if (!string.IsNullOrEmpty(filter.City))
                query = query.Where(a => a.City.Contains(filter.City));

            // Apply other filters as needed

            var addresses = await query.ToListAsync();
            return addresses.Select(a => MapToAddressResponseDto(a));
        }

        public async Task<addressResponseDTO> CreateAddressAsync(addressCreateDTO addressDto)
        {
            var address = new Address
            {
                AddressLine1 = addressDto.AddressLine1,
                AddressLine2 = addressDto.AddressLine2,
                City = addressDto.City,
                State = addressDto.State,
                Country = addressDto.Country,
                ZipCode = addressDto.ZipCode,
                CreatedBy = addressDto.CreatedBy
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return MapToAddressResponseDto(address);
        }

        public async Task<addressResponseDTO> UpdateAddressAsync(Guid id, addressUpdateDTO addressDto)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                throw new Exception($"Address with ID {id} not found");

            address.AddressLine1 = addressDto.AddressLine1;
            address.AddressLine2 = addressDto.AddressLine2;
            address.City = addressDto.City;
            address.State = addressDto.State;
            address.Country = addressDto.Country;
            address.ZipCode = addressDto.ZipCode;
            address.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToAddressResponseDto(address);
        }

        public async Task<bool> DeleteAddressAsync(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                throw new Exception($"Address with ID {id} not found");

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return true;
        }

        private addressResponseDTO MapToAddressResponseDto(Address address)
        {
            if (address == null)
                return null;

            return new addressResponseDTO
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                ZipCode = address.ZipCode,
                CreatedAt = address.CreatedAt,
                UpdatedAt = address.UpdatedAt
            };
        }
    }

}
