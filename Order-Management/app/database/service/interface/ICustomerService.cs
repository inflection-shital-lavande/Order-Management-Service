using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.cutomerModelDTO;

namespace Order_Management.app.database.service
{
    public interface ICustomerService
    {
        Task<customerResponseDTO> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<customerResponseDTO>> GetAllCustomersAsync();
        Task<IEnumerable<customerResponseDTO>> SearchCustomersAsync(customerSearchFilterDTO filter);
        Task<customerResponseDTO> CreateCustomerAsync(customerCreateDTO customerDto);
        Task<customerResponseDTO> UpdateCustomerAsync(Guid id, customerUpdateDTO customerDto);
        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
