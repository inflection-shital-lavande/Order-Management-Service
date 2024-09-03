using order_management.database.dto;
using order_management.database.models;

namespace Order_Management.src.services.interfaces;

public interface ICustomerAddress
{
    Task<List<CustomerAddress>> GetAllCustomerAddresses();
    
    Task<CustomerAddress> Create(CustomerAddressCreateDTO customerAddressDto);
}
