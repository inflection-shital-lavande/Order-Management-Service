using AutoMapper;
using order_management.database.dto;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order_Management.src.services.interfaces;
using System.Net;
using Order_Management.src.database.dto.merchant;

namespace Order_Management.src.services.implementetions
{
    public class CartService :ICartService
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public CartService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task<List<CartResponseModel>> GetAll() =>

        //    _mapper.Map<List<CartResponseModel>>(await _context.Carts.ToListAsync());


        public async Task<List<CartResponseModel>> GetAll()
        {
            var carts = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Order)

                .ToListAsync();

            return _mapper.Map<List<CartResponseModel>>(carts);
        }
      

        public async Task<CartResponseModel> GetById(Guid id)
        {
            var cart = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Customer)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(a => a.Id == id);

            return cart != null ? _mapper.Map<CartResponseModel>(cart) : null;
        }


        public async Task<CartSearchResults> Search(CartSearchFilter filter)
        {
            if (_context.Carts == null)
                return new CartSearchResults { Items = new List<CartResponseModel>() };

            var query = _context.Carts.AsQueryable();

            // Apply filters to the query
            if (filter.CustomerId.HasValue)
                query = query.Where(c => c.CustomerId == filter.CustomerId.Value);

            //if (filter.ProductId.HasValue)
            //    query = query.Where(c => c.CartItems.Any(ci => ci.ProductId == filter.ProductId.Value));

            if (filter.TotalItemsCountGreaterThan.HasValue)
                query = query.Where(c => c.TotalItemsCount > filter.TotalItemsCountGreaterThan.Value);

            if (filter.TotalItemsCountLessThan.HasValue)
                query = query.Where(c => c.TotalItemsCount < filter.TotalItemsCountLessThan.Value);

            if (filter.TotalAmountGreaterThan.HasValue)
                query = query.Where(c => c.TotalAmount > filter.TotalAmountGreaterThan.Value);

            if (filter.TotalAmountLessThan.HasValue)
                query = query.Where(c => c.TotalAmount < filter.TotalAmountLessThan.Value);

            if (filter.CreatedBefore.HasValue)
                query = query.Where(c => c.CreatedAt < filter.CreatedBefore.Value);

            if (filter.CreatedAfter.HasValue)
                query = query.Where(c => c.CreatedAt > filter.CreatedAfter.Value);



            var cart = await query.ToListAsync();
            var results = _mapper.Map<List<CartResponseModel>>(cart);

            return new CartSearchResults { Items = results };
        }

        public async Task<CartResponseModel> Create(CartCreateModel create)
        {
            var cart = _mapper.Map<Cart>(create);
            cart.CreatedAt = DateTime.UtcNow;
            cart.UpdatedAt = DateTime.UtcNow;

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return _mapper.Map<CartResponseModel>(cart);
        }

        public async Task<CartResponseModel> Update(Guid id, CartUpdateModel update)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) return null;

            _mapper.Map(update, cart);
            cart.UpdatedAt = DateTime.UtcNow;

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            return _mapper.Map<CartResponseModel>(cart);
        }

        public async Task<bool> Delete(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return true;
        }

    }


}
