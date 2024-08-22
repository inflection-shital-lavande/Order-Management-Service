using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.merchant;
using order_management.src.database.dto.orderHistory;
using Order_Management.src.services.interfaces;
using Order_Management.src.database.dto.payment_transaction;
using Microsoft.EntityFrameworkCore;

namespace Order_Management.src.services.implementetions
{
    public class PaymentTransactionService :IPaymentTransactionService
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public PaymentTransactionService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PaymentTransactionResponseModel>> GetAll() =>

           _mapper.Map<List<PaymentTransactionResponseModel>>(await _context.PaymentTransactions.ToListAsync());



        public async Task<PaymentTransactionResponseModel> GetById(Guid id)
        {
            var address = await _context.PaymentTransactions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return address != null ? _mapper.Map<PaymentTransactionResponseModel>(address) : null;
        }


        public async Task<PaymentTransactionSearchResults> Search(PaymentTransactionSearchFilter filter)
        {
            if (_context.PaymentTransactions == null)
                return new PaymentTransactionSearchResults { Items = new List<PaymentTransactionResponseModel>() };

            var query = _context.PaymentTransactions.AsQueryable();

            if (!string.IsNullOrEmpty(filter.InvoiceNumber))
                query = query.Where(a => a.InvoiceNumber.Contains(filter.InvoiceNumber));


            var addresses = await query.ToListAsync();
            var results = _mapper.Map<List<PaymentTransactionResponseModel>>(addresses);

            return new PaymentTransactionSearchResults { Items = results };
        }

        public async Task<PaymentTransactionResponseModel> Create(PaymentTransactionCreateModel create)
        {
            var address = _mapper.Map<PaymentTransaction>(create);
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            _context.PaymentTransactions.Add(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentTransactionResponseModel>(address);
        }

       
        public async Task<bool> Delete(Guid id)
        {
            var address = await _context.PaymentTransactions.FindAsync(id);
            if (address == null) return false;

            _context.PaymentTransactions.Remove(address);
            await _context.SaveChangesAsync();

            return true;
        }

    }


}
