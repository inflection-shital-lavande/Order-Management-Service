using order_management.auth;
using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.order_line_item;

namespace order_management.services.interfaces;

public interface ICustomerService
{

     Task<List<CustomerResponseModel>> GetAll();
    //Task<IEnumerable<Customer>> GetAll();

    Task<CustomerResponseModel> GetById(Guid id);

    Task<CustomerResponseModel> Create(CustomerCreateModel customerCreate);
    Task<CustomerResponseModel> Update(Guid id, CustomerUpdateModel customerUpdate);
    Task<bool> Delete(Guid id);
    Task<CustomerSearchResultsModel> Search(CustomerSearchFilterModel filter);


    

}




