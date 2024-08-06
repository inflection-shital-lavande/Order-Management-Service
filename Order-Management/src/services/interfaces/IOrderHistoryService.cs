using Order_Management.Auth;
using Order_Management.database.dto;
using Order_Management.src.database.dto.orderHistory;

namespace Order_Management.src.services.interfaces
{
    public interface IOrderHistoryService
    {
        Task<OrderHistoryResponseDTO> GetCustomerByIdAsync(Guid id);
        Task<List<OrderHistoryResponseDTO>> GetAllCustomersAsync();
        Task<List<OrderHistorySearchResultsDTO>> SearchCustomersAsync(OrderHistorySearchFilterDTO filter);
        Task<Response> CreateCustomerAsync(OrderHistoryCreateDTO customerDto);
        Task<Response> UpdateCustomerAsync(Guid id, OrderHistoryUpdateDTO customerDto);
        Task<Response> DeleteCustomerAsync(Guid id);
    }
}
