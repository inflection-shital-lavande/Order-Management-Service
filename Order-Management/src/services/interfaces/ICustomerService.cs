using Order_Management.Auth;
using Order_Management.database.dto;

namespace Order_Management.services.interfaces
{
    public interface ICustomerService
    {

        Task<CustomerResponseDTO> GetCustomerByIdAsync(Guid id);
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        Task<List<CustomerSearchResultsDTO>> SearchCustomersAsync(CustomerSearchFilterDTO filter);
        Task<Response> CreateCustomerAsync(CustomerCreateDTO customerDto);
        Task<Response> UpdateCustomerAsync(Guid id, CustomerUpdateDTO customerDto);
        Task<Response> DeleteCustomerAsync(Guid id);
    }
}
