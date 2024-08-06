using AutoMapper;

using Order_Management.Auth;
using Microsoft.EntityFrameworkCore;
using Order_Management.services.interfaces;
using Order_Management.database;
using Order_Management.database.dto;
using Order_Management.database.models;

namespace Order_Management.services.implementetions
{
    public class CustomerService : ICustomerService
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public CustomerService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _context.Customers
                .Include(c => c.DefaultShippingAddress)
                .Include(c => c.DefaultBillingAddress)
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<CustomerResponseDTO>(customer);
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.DefaultShippingAddress)
                .Include(c => c.DefaultBillingAddress)
                .ToListAsync();

            return _mapper.Map<List<CustomerResponseDTO>>(customers);
        }

        public async Task<List<CustomerSearchResultsDTO>> SearchCustomersAsync(CustomerSearchFilterDTO filter)
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
            return _mapper.Map<List<CustomerSearchResultsDTO>>(customers);
        }

        public async Task<Response> CreateCustomerAsync(CustomerCreateDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new Response
            {

                Message = "Customer created successfully"

            };
        }

        public async Task<Response> UpdateCustomerAsync(Guid id, CustomerUpdateDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return new Response
                {

                    Message = $"Customer with ID {id} not found"
                };

            _mapper.Map(customerDto, customer);
            await _context.SaveChangesAsync();

            return new Response
            {

                Message = "Customer updated successfully"

            };
        }

        public async Task<Response> DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return new Response
                {

                    Message = $"Customer with ID {id} not found"
                };

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new Response
            {
                Message = "Customer deleted successfully"
            };
        }
    }
}

