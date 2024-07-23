using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.cutomerModelDTO;
using Order_Management.Auth;

namespace Order_Management.app.database.service
{
    public interface ICustomerService
    {
        Task<customerResponseDTO> GetCustomerByIdAsync(Guid id);
        Task<List<customerResponseDTO>> GetAllCustomersAsync();
        Task<List<customerSearchResultsDTO>> SearchCustomersAsync(customerSearchFilterDTO filter);
        Task<Response> CreateCustomerAsync(customerCreateDTO customerDto);
        Task<Response> UpdateCustomerAsync(Guid id, customerUpdateDTO customerDto);
        Task<Response> DeleteCustomerAsync(Guid id);
    }
}


