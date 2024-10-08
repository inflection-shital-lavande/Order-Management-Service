﻿using AutoMapper;

using order_management.auth;
using Microsoft.EntityFrameworkCore;
using order_management.services.interfaces;
using order_management.database;
using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.merchant;
using order_management.domain_types.enums;

namespace order_management.services.implementetions;

public class CustomerService : ICustomerService
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public CustomerService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerResponseModel> GetById(Guid id)
    {
        var customer = await _context.Customers
            .Include(c => c.DefaultShippingAddress)
            .Include(c => c.DefaultBillingAddress)
            .FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map<CustomerResponseModel>(customer);
    }

    public async Task<List<CustomerResponseModel>> GetAll()
    {
        var customers = await _context.Customers
            .Include(c => c.DefaultShippingAddress)
            .Include(c => c.DefaultBillingAddress)
            .ToListAsync();

        return _mapper.Map<List<CustomerResponseModel>>(customers);
    }


    private async Task<CustomerAddress> AddCustomerAddress(Guid customerId, Guid addressId, AddressTypes addressType, bool isFavorite = true)
    {
        // Check if the customer exists
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
            throw new Exception($"Customer with id {customerId} not found");

        // Check if the address exists
        var address = await _context.Addresses.FindAsync(addressId);
        if (address == null)
            throw new Exception($"Address with id {addressId} not found");

        // Create a new CustomerAddress instance
        var customerAddress = new CustomerAddress
        {
            CustomerId = customerId,
            AddressId = addressId,
            AddressType = addressType,
            IsFavorite = isFavorite
        };

        // Add the new customer address to the database
        _context.CustomerAddresses.Add(customerAddress);
        await _context.SaveChangesAsync();

        return customerAddress;
    }



    public async Task<CustomerSearchResultsModel> Search(CustomerSearchFilterModel filter)
    {

        if (_context.Customers == null)
            return new CustomerSearchResultsModel { Items = new List<CustomerResponseModel>() };

        var query = _context.Customers
             .Include(c => c.DefaultShippingAddress)
             .Include(c => c.DefaultBillingAddress)
             .AsQueryable();

        // Apply filters to the query
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

        //if (filter.CreatedBefore.HasValue)
        //    query = query.Where(c => c.CreatedDate < filter.CreatedBefore.Value);

        //if (filter.CreatedAfter.HasValue)
        //    query = query.Where(c => c.CreatedDate > filter.CreatedAfter.Value);

        //if (filter.PastMonths.HasValue)
        //{
        //    var pastDate = DateTime.Now.AddMonths(-filter.PastMonths.Value);
        //    query = query.Where(c => c.CreatedDate >= pastDate);
        //}


        var customers = await query.ToListAsync();


        var results = _mapper.Map<List<CustomerResponseModel>>(customers);


        return new CustomerSearchResultsModel { Items = results };
    }

    


    public async Task<CustomerResponseModel> Create(CustomerCreateModel Create)
    {
        var customer = _mapper.Map<Customer>(Create);
        customer.CreatedAt = DateTime.UtcNow;
        customer.UpdatedAt = DateTime.UtcNow;

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerResponseModel>(customer);
    }


    public async Task<CustomerResponseModel> Update(Guid id, CustomerUpdateModel customerUpdate)
    {
        var existingCustomer = await _context.Customers.FindAsync(id);
        if (existingCustomer == null) return null;

        _mapper.Map(customerUpdate, existingCustomer);
        existingCustomer.UpdatedAt = DateTime.UtcNow;

        _context.Customers.Update(existingCustomer);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerResponseModel>(existingCustomer);
    }
    public async Task<bool> Delete(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return true;
    }
    
    
}

