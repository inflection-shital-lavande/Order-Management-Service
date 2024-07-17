

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Order_Management.app.database.models;
using Order_Management.app.domain_types.dto;
using Order_Management.Auth;
using Order_Management.Data;

namespace Order_Management.app.database.service
{

    public class AddressService : IAddressService
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public AddressService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<addressResponseDTO> GetAddressByIdAsync(Guid id) =>
            _mapper.Map<addressResponseDTO>(await _context.Addresses.FindAsync(id));

        public async Task<List<addressResponseDTO>> GetAll() =>

           _mapper.Map<List<addressResponseDTO>>(await _context.Addresses.ToListAsync());


        public async Task<Response> CreateAddressAsync(addressCreateDTO addressCreateDTO)
        {
            _context.Addresses.Add(_mapper.Map<Address>(addressCreateDTO));
            await _context.SaveChangesAsync();
            return new Response(true, "saved");
        }
        public async Task<Response> UpdateAddressAsync(Guid id, addressUpdateDTO request)
        {
            _context.Addresses.Update(_mapper.Map<Address>(request));
            await _context.SaveChangesAsync();
            return new Response(true, "Update");
        }

        public async Task<Response> DeleteAddressAsync(Guid id)
        {
            _context.Addresses.Remove(await _context.Addresses.FindAsync(id));
            await _context.SaveChangesAsync();
            return new Response(true, "Delete");
        }


        public async Task<List<addressSearchResultsDTO>> SearchAddressesAsync(addressSearchFilterDTO filter)
        {

            var query = _context.Addresses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.AddressLine1))
                query = query.Where(a => a.AddressLine1.Contains(filter.AddressLine1));

            if (!string.IsNullOrEmpty(filter.AddressLine2))
                query = query.Where(a => a.AddressLine2.Contains(filter.AddressLine2));


            if (!string.IsNullOrEmpty(filter.City))
                query = query.Where(a => a.City.Contains(filter.City));


            if (!string.IsNullOrEmpty(filter.State))
                query = query.Where(a => a.State.Contains(filter.State));

            if (!string.IsNullOrEmpty(filter.Country))
                query = query.Where(a => a.Country.Contains(filter.Country));

            if (!string.IsNullOrEmpty(filter.ZipCode))
                query = query.Where(a => a.ZipCode.Contains(filter.ZipCode));




            var results = await query
                .Select(a => _mapper.Map<addressSearchResultsDTO>(a))
                .ToListAsync();

            return results;
        }
    }
}
