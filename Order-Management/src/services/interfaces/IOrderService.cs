using order_management.auth;
using order_management.database.dto;
using order_management.database.models;
using order_management.domain_types.enums;
using order_management.src.database.dto;
using Order_Management.src.database.dto.orderType;

namespace order_management.src.services.interfaces;

public interface IOrderService
{
     Task<List<OrderResponseModel>> GetAll();
    //Task<IEnumerable<Order>> GetAll();
    Task<OrderResponseModel> GetById(Guid id);

    Task<OrderResponseModel> Create(OrderCreateModel create);
    Task<OrderResponseModel> Update(Guid id, OrderUpdateModel update);


    Task<bool> Delete(Guid id);
    Task<OrderSearchResultsModel> Search(OrderSearchFilterModel filter);
   Task <OrderResponseModel> UpdateOrderStatus(Guid orderId, OrderStatusTypes status);



}


