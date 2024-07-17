using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order_Management.app.database.models;
using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.cutomerModelDTO;
using Order_Management.Data;

namespace Order_Management.app.database.service
{
    public class CustomerService : ICustomerService
    {
        private readonly OrderManagementContext _context;

        public CustomerService(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<customerResponseDTO> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _context.Customers
                .Include(c => c.DefaultShippingAddress)
                .Include(c => c.DefaultBillingAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            return MapToCustomerResponseDto(customer);
        }

        public async Task<IEnumerable<customerResponseDTO>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.DefaultShippingAddress)
                .Include(c => c.DefaultBillingAddress)
                .ToListAsync();

            return customers.Select(c => MapToCustomerResponseDto(c));
        }

        public async Task<IEnumerable<customerResponseDTO>> SearchCustomersAsync(customerSearchFilterDTO filter)
        {
            var query = _context.Customers
                .Include(c => c.DefaultShippingAddress)
                .Include(c => c.DefaultBillingAddress)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(c => c.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(c => c.Email.Contains(filter.Email));

            if (!string.IsNullOrEmpty(filter.PhoneCode))
                query = query.Where(c => c.PhoneCode.Contains(filter.PhoneCode));

            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(c => c.Phone.Contains(filter.Phone));

            if (!string.IsNullOrEmpty(filter.TaxNumber))
                query = query.Where(c => c.TaxNumber.Contains(filter.TaxNumber));

          


            var customers = await query.ToListAsync();
            return customers.Select(c => MapToCustomerResponseDto(c));
        }

        public async Task<customerResponseDTO> CreateCustomerAsync(customerCreateDTO customerDto)
        {
            var customer = new Customer
            {
                ReferenceId = customerDto.ReferenceId,
                Name = customerDto.Name,
                Email = customerDto.Email,
                PhoneCode = customerDto.PhoneCode,
                Phone = customerDto.Phone,
                ProfilePicture = customerDto.ProfilePicture,
                TaxNumber = customerDto.TaxNumber,
                DefaultShippingAddressId = customerDto.DefaultShippingAddressId,
                DefaultBillingAddressId = customerDto.DefaultBillingAddressId
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return MapToCustomerResponseDto(customer);
        }

        public async Task<customerResponseDTO> UpdateCustomerAsync(Guid id, customerUpdateDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new Exception($"Customer with ID {id} not found");

            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.PhoneCode = customerDto.PhoneCode;
            customer.Phone = customerDto.Phone;
            customer.ProfilePicture = customerDto.ProfilePicture;
            customer.TaxNumber = customerDto.TaxNumber;
            

            await _context.SaveChangesAsync();

            return MapToCustomerResponseDto(customer);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                throw new Exception($"Customer with ID {id} not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        private customerResponseDTO MapToCustomerResponseDto(Customer customer)
        {
            if (customer == null)
                return null;

            return new customerResponseDTO
            {
                Id = customer.Id,
                ReferenceId = customer.ReferenceId,
                Name = customer.Name,
                Email = customer.Email,
                PhoneCode = customer.PhoneCode,
                Phone = customer.Phone,
                ProfilePicture = customer.ProfilePicture,
                TaxNumber = customer.TaxNumber,
                DefaultShippingAddressId = customer.DefaultShippingAddressId,
                DefaultShippingAddress = MapToAddressDictionary(customer.DefaultShippingAddress),
                DefaultBillingAddressId = customer.DefaultBillingAddressId,
                DefaultBillingAddress = MapToAddressDictionary(customer.DefaultBillingAddress),
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        private Dictionary<string, object> MapToAddressDictionary(Address address)
        {
            if (address == null)
                return null;

            return new Dictionary<string, object>
            {
                { "Id", address.Id },
                { "AddressLine1", address.AddressLine1 },
                { "AddressLine2", address.AddressLine2 },
                { "City", address.City },
                { "State", address.State },
                { "Country", address.Country },
                { "ZipCode", address.ZipCode }
                
            };
        }
    }
}
