using AutoMapper;
using order_management.database.dto;
using order_management.database.models;
using order_management.database;
using static Order_Management.src.services.implementetions.CustomerAddressService;
using Order_Management.src.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Order_Management.src.services.implementetions;

public class CustomerAddressService: ICustomerAddress
{

  
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public CustomerAddressService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


    public async Task<List<CustomerAddress>> GetAllCustomerAddresses()
    {
        var customers = await _context.CustomerAddresses
            .Include(c => c.Customers)
            .Include(c => c.Addresses)
            .ToListAsync();

        return _mapper.Map<List<CustomerAddress>>(customers);
    }


    
    public async Task<CustomerAddress> Create(CustomerAddressCreate customerAddressDto)
    {
        var customerAddress = _mapper.Map<CustomerAddress>(customerAddressDto);
        _context.CustomerAddresses.Add(customerAddress);
        await _context.SaveChangesAsync();
        return customerAddress;
    }




}


