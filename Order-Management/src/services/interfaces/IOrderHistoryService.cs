using order_management.auth;
using order_management.database.dto;
using order_management.database.models;
using order_management.src.database.dto.orderHistory;
using Order_Management.src.database.dto.order_line_item;

namespace order_management.src.services.interfaces;

public interface IOrderHistoryService
{
    Task<List<OrderHistoryResponseModel>> GetAll();
    //Task<IEnumerable<OrderHistory>> GetAll();

    Task<OrderHistoryResponseModel> GetById(Guid id);
    Task<OrderHistoryResponseModel> Create(OrderHistoryCreateModel customerDto);
    Task<OrderHistoryResponseModel> Update(Guid id, OrderHistoryUpdateModel customerDto);
    Task<bool> Delete(Guid id);
    Task<OrderHistorySearchResultsModel> Search(OrderHistorySearchFilterModel filter);



    

}
