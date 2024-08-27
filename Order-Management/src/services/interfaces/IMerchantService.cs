using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.merchant;

namespace Order_Management.src.services.interfaces
{
    public interface IMerchantService
    {
       // Task<List<MerchantResponseModel>> GetAll();
        Task<List<MerchantResponseModel>> GetAll();

        Task<MerchantResponseModel> GetById(Guid id);

        Task<MerchantResponseModel> Create(MerchantCreateModel Create);
        Task<MerchantResponseModel> Update(Guid id, MerchantUpdateModel Update);
        Task<bool> Delete(Guid id);
        Task<MerchantSearchResults> Search(MerchantSearchFilter filter);

        //Task<bool> AddressExists(Guid addressId);

    }
}
