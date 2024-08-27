using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.cart;

namespace Order_Management.src.services.interfaces;

public interface ICartService
{
    Task<List<CartResponseModel>> GetAll();//IEnumerable
   // Task<IEnumerable<Cart>> GetAll();

    Task<CartResponseModel> GetById(Guid id);
    Task<CartResponseModel> Create(CartCreateModel create);//CartResponseDTO
    Task<CartResponseModel> Update(Guid id, CartUpdateModel Update);

    Task<bool> Delete(Guid id);
    Task<CartSearchResults> Search(CartSearchFilter filter);

}
