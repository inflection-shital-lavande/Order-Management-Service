using order_management.database.models;
using order_management.src.database.dto;
using Order_Management.src.database.dto.order_line_item;

namespace Order_Management.src.services.interfaces
{
    public interface IOrderLineItem
    {
        Task<List<OrderLineItemResponseModel>> GetAll();
       // Task<IEnumerable<OrderLineItem>> GetAll();
        Task<OrderLineItemResponseModel> GetById(Guid id);

        Task<OrderLineItemResponseModel> Create(OrderLineItemCreateModel create);
        Task<OrderLineItemResponseModel> Update(Guid id, OrderLineItemUpdateModel update);


        Task<bool> Delete(Guid id);
        Task<OrderLineItemSearchResults> Search(OrderLineItemSearchFilter filter);

       
    }
}
