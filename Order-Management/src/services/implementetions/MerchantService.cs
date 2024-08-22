using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.cart;
using Order_Management.src.database.dto.merchant;
using Microsoft.EntityFrameworkCore;
using Order_Management.src.services.interfaces;

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

        public async Task<List<MerchantResponseModel>> GetAll() =>

           _mapper.Map<List<MerchantResponseModel>>(await _context.Merchants.ToListAsync());



        public async Task<MerchantResponseModel> GetById(Guid id)
        {
            var address = await _context.Merchants
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return address != null ? _mapper.Map<MerchantResponseModel>(address) : null;
        }


        public async Task<MerchantSearchResults> Search(MerchantSearchFilter filter)
        {
            if (_context.Merchants == null)
                return new MerchantSearchResults { Items = new List<MerchantResponseModel>() };

            var query = _context.Merchants.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(a => a.Name.Contains(filter.Name));


            var addresses = await query.ToListAsync();
            var results = _mapper.Map<List<MerchantResponseModel>>(addresses);

            return new MerchantSearchResults { Items = results };
        }

        public async Task<MerchantResponseModel> Create(MerchantCreateModel create)
        {
            var address = _mapper.Map<Merchant>(create);
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            _context.Merchants.Add(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<MerchantResponseModel>(address);
        }

        public async Task<MerchantResponseModel> Update(Guid id, MerchantUpdateModel update)
        {
            var address = await _context.Merchants.FindAsync(id);
            if (address == null) return null;

            _mapper.Map(update, address);
            address.UpdatedAt = DateTime.UtcNow;

            _context.Merchants.Update(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<MerchantResponseModel>(address);
        }

        public async Task<bool> Delete(Guid id)
        {
            var address = await _context.Merchants.FindAsync(id);
            if (address == null) return false;

            _context.Merchants.Remove(address);
            await _context.SaveChangesAsync();

            return true;
        }

    }


}