using order_management.database.dto;
using order_management.database.models;

namespace Order_Management.src.services.interfaces;

public interface ICustomerAddress
{
    Task<IEnumerable<CustomerAddress>> GetAllCustomerAddressesAsync();
    
    Task<CustomerAddress> CreateCustomerAddressAsync(CustomerAddressCreateDTO customerAddressDto);
}
