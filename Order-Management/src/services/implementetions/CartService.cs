using AutoMapper;
using order_management.database.dto;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order_Management.src.services.interfaces;
using System.Net;

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

        public async Task<List<CartResponseModel>> GetAll() =>

            _mapper.Map<List<CartResponseModel>>(await _context.Carts.ToListAsync());


        //public async Task<IEnumerable<Cart>> GetAll()
        //{
        //    return await _context.Carts
        //        .Include(o => o.Orders)
        //        .Include(ol => ol.OrderLineItems)
        //        .Select(c => _mapper.Map<Cart>(c))
        //        .ToListAsync();
        //}
        public async Task<CartResponseModel> GetById(Guid id)
        {
            var cart = await _context.Carts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return cart != null ? _mapper.Map<CartResponseModel>(cart) : null;
        }


        public async Task<CartSearchResults> Search(CartSearchFilter filter)
        {
            if (_context.Carts == null)
                return new CartSearchResults { Items = new List<CartResponseModel>() };

            var query = _context.Carts.AsQueryable();

            //if (!string.IsNullOrEmpty(filter.CustomerId))
            //    query = query.Where(a => a.CustomerId.Contains(filter.CustomerId));
          

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
