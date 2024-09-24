using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.cart;
using Order_Management.src.database.dto.merchant;
using Microsoft.EntityFrameworkCore;
using Order_Management.src.services.interfaces;
using order_management.database.dto;
using System.Data;
using order_management.common;
using Order_Management.src.common;

namespace Order_Management.src.services.implementetions
{
    public class MerchantService: IMerchantService
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public MerchantService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        


        public async Task<List<MerchantResponseModel>> GetAll()
        {
            var merchants = await _context.Merchants
                .Include(c => c.Addressess)
                
                .ToListAsync();

            return _mapper.Map<List<MerchantResponseModel>>(merchants);
        }
        public async Task<MerchantResponseModel> GetById(Guid id)
        {
            var merchants = await _context.Merchants
                .AsNoTracking()
                .Include(c => c.Addressess)
                .FirstOrDefaultAsync(a => a.Id == id);

            return merchants != null ? _mapper.Map<MerchantResponseModel>(merchants) : null;
        }


        public async Task<MerchantSearchResults> Search(MerchantSearchFilter filter)
        {
            if (_context.Merchants == null)
                return new MerchantSearchResults { Items = new List<MerchantResponseModel>() };

            var query = _context.Merchants.AsQueryable();

            // Apply filters to the query
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(m => m.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(m => m.Email.Contains(filter.Email));

            if (!string.IsNullOrEmpty(filter.Phone))
                query = query.Where(m => m.Phone.Contains(filter.Phone));

            if (!string.IsNullOrEmpty(filter.TaxNumber))
                query = query.Where(m => m.TaxNumber.Contains(filter.TaxNumber));

            //if (filter.CreatedBefore.HasValue)
            //    query = query.Where(m => m.CreatedDate < filter.CreatedBefore.Value);

            //if (filter.CreatedAfter.HasValue)
            //    query = query.Where(m => m.CreatedDate > filter.CreatedAfter.Value);

            //if (filter.PastMonths.HasValue)
            //{
            //    var pastDate = DateTime.Now.AddMonths(-filter.PastMonths.Value);
            //    query = query.Where(m => m.CreatedDate >= pastDate);
            //}


            var merchants = await query.ToListAsync();
            var results = _mapper.Map<List<MerchantResponseModel>>(merchants);

            return new MerchantSearchResults { Items = results };
        }

   
         public async Task<MerchantResponseModel> Create(MerchantCreateModel Create)
           {
            if (!string.IsNullOrEmpty(Create.Email))
            {
                var existingMerchant = await _context.Merchants
                    .Where(m => m.Email.ToLower() == Create.Email.ToLower())
                    .FirstOrDefaultAsync();

                if (existingMerchant != null)
                {
                    throw new ConflictException( $"Merchant with email {Create.Email} already exists!");
                }
            }

            // Check for existing phone
            if (!string.IsNullOrEmpty(Create.Phone))
            {
                var existingMerchant = await _context.Merchants
                    .Where(m => m.Phone == Create.Phone)
                    .FirstOrDefaultAsync();

                if (existingMerchant != null)
                {
                    throw new ConflictException( $"Merchant with phone {Create.Phone} already exists!");
                }
            }

            // Check for existing tax number
            if (!string.IsNullOrEmpty(Create.TaxNumber))
            {
                var existingMerchant = await _context.Merchants
                    .Where(m => m.TaxNumber.ToLower() == Create.TaxNumber.ToLower())
                    .FirstOrDefaultAsync();

                if (existingMerchant != null)
                {
                    throw new ConflictException( $"Merchant with tax number {Create.TaxNumber} already exists!");
                }
            }

            // Check for existing GST number
            if (!string.IsNullOrEmpty(Create.GSTNumber))
            {
                var existingMerchant = await _context.Merchants
                    .Where(m => m.GSTNumber.ToLower() == Create.GSTNumber.ToLower())
                    .FirstOrDefaultAsync();

                if (existingMerchant != null)
                {
                  
                    throw new ConflictException( $"Merchant with GST number {Create.GSTNumber} already exists!");
                }
            }

            //////
            var merchants = _mapper.Map<Merchant>(Create);
              merchants.CreatedAt = DateTime.UtcNow;
              merchants.UpdatedAt = DateTime.UtcNow;

             _context.Merchants.Add(merchants);
              await _context.SaveChangesAsync();

            return _mapper.Map<MerchantResponseModel>(merchants);
           }
        

        public async Task<MerchantResponseModel> Update(Guid id, MerchantUpdateModel update)
        {
            var merchants = await _context.Merchants.FindAsync(id);
            if (merchants == null) return null;

            _mapper.Map(update, merchants);
            merchants.UpdatedAt = DateTime.UtcNow;

            _context.Merchants.Update(merchants);
            await _context.SaveChangesAsync();

            return _mapper.Map<MerchantResponseModel>(merchants);
        }

        public async Task<bool> Delete(Guid id)
        {
            var merchants = await _context.Merchants.FindAsync(id);
            if (merchants == null) return false;

            _context.Merchants.Remove(merchants);
            await _context.SaveChangesAsync();

            return true;
        }

       
    }


}