using order_management.auth;
using order_management.common;
using order_management.database.dto;
using order_management.database.models;
using System.Threading.Tasks;

namespace order_management.services.interfaces;

public interface IAddressService
{
    // Task<IEnumerable<Cart>> GetAll();
    Task<List<AddressResponseModel>> GetAll();

    Task<AddressResponseModel> GetById(Guid id);

    Task<AddressResponseModel> Create(AddressCreateModel create);
    Task<AddressResponseModel> Update(Guid id, AddressUpdateModel update);
    Task<bool> Delete(Guid id);
    Task<AddressSearchResultsModel> Search(AddressSearchFilterModel filter);


}



