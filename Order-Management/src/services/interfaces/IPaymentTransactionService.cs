using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.payment_transaction;

namespace Order_Management.src.services.interfaces
{
    public interface IPaymentTransactionService
    {
        //Task<IEnumerable<PaymentTransaction>> GetAll();
        Task<List<PaymentTransactionResponseModel>> GetAll();

        Task<PaymentTransactionResponseModel> GetById(Guid id);
        Task<PaymentTransactionResponseModel> Create(PaymentTransactionCreateModel create);       
        Task<bool> Delete(Guid id);
        Task<PaymentTransactionSearchResults> Search(PaymentTransactionSearchFilter filter);

    }
}
