using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.orderType;

namespace Order_Management.src.services.interfaces;


    public interface IOrderTypeService
    {
       
         Task<List<OrderTypeResponseModel>> GetAll();
        //Task<IEnumerable<OrderType>> GetAll();
        Task<OrderTypeResponseModel> GetById(Guid id);

        Task<OrderTypeResponseModel> Create(OrderTypeCreateModel create);
        Task<OrderTypeResponseModel> Update(Guid id, OrderTypeUpdateModel update);
        Task<bool> Delete(Guid id);

        Task<OrderTypeSearchResults> Search(OrderTypeSearchFilter filter);

}

