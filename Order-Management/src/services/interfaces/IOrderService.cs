using Order_Management.Auth;
using Order_Management.database.dto;
using Order_Management.src.database.dto;

namespace Order_Management.src.services.interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> GetOrderByIdAsync(Guid id);
        Task<List<OrderResponseDTO>> GetAll();

        Task<Response> CreateOrderAsync(OrderCreateDTO addressDto);
        Task<Response> UpdateOrderAsync(Guid id, OrderUpdateDTO addressDto);


        Task<Response> DeleteOrderAsync(Guid id);
        Task<List<OrderSearchResultsDTO>> SearchOrderAsync(OrderSearchFilterDTO filter);
    }
}


