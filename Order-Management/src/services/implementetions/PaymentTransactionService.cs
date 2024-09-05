using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.merchant;
using order_management.src.database.dto.orderHistory;
using Order_Management.src.services.interfaces;
using Order_Management.src.database.dto.payment_transaction;
using Microsoft.EntityFrameworkCore;
using order_management.src.database.dto;
using Order_Management.src.database.dto.order_line_item;

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

        

       

        public async Task<List<PaymentTransactionResponseModel>> GetAll()
        {
            var paymentTransactions = await _context.PaymentTransactions
                 .Include(pt => pt.Order) // Include related Order
                .Include(pt => pt.Customer) // Include related Customer

                .ToListAsync();
            // Map to response model
            return _mapper.Map<List<PaymentTransactionResponseModel>>(paymentTransactions);
        }
        public async Task<PaymentTransactionResponseModel> GetById(Guid id)
        {
            var transaction = await _context.PaymentTransactions
                .Include(pt => pt.Order)          // Eagerly load the related Order
                .Include(pt => pt.Customer)       // Eagerly load the related Customer
                .AsNoTracking()
                .FirstOrDefaultAsync(pt => pt.Id == id);

            return transaction != null ? _mapper.Map<PaymentTransactionResponseModel>(transaction) : null;
        }


       

        public async Task<PaymentTransactionSearchResults> Search(PaymentTransactionSearchFilter filter)
        {
            var query = _context.PaymentTransactions.AsQueryable();

            if (!string.IsNullOrEmpty(filter.DisplayCode))
                query = query.Where(pt => pt.DisplayCode.Contains(filter.DisplayCode));

            if (!string.IsNullOrEmpty(filter.InvoiceNumber))
                query = query.Where(pt => pt.InvoiceNumber.Contains(filter.InvoiceNumber));

            if (filter.BankTransactionId.HasValue)
                query = query.Where(pt => pt.BankTransactionId == filter.BankTransactionId.Value);

            if (filter.InitiatedDate.HasValue)
                query = query.Where(x => x.InitiatedDate == filter.InitiatedDate.Value);

            if (filter.CustomerId.HasValue)
                query = query.Where(pt => pt.CustomerId == filter.CustomerId.Value);

            if (filter.OrderId.HasValue)
                query = query.Where(pt => pt.OrderId == filter.OrderId.Value);

            if (!string.IsNullOrEmpty(filter.PaymentMode))
                query = query.Where(x => x.PaymentMode == filter.PaymentMode);

            if (filter.PaymentAmount.HasValue)
                query = query.Where(pt => pt.PaymentAmount == filter.PaymentAmount.Value);

            if (filter.IsRefund.HasValue)
                query = query.Where(pt => pt.IsRefund == filter.IsRefund.Value);

            var totalCount = await query.CountAsync();
            var items = await query.ToListAsync();
            var results = _mapper.Map<List<PaymentTransactionResponseModel>>(items);

            return new PaymentTransactionSearchResults { Items = results };

        }
       
            public async Task<PaymentTransactionResponseModel> Create(PaymentTransactionCreateModel create)
        {
            var order = _mapper.Map<PaymentTransaction>(create);
            _context.PaymentTransactions.Add(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentTransactionResponseModel>(order);

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
