﻿using AutoMapper;
using order_management.database.dto;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order_Management.src.services.interfaces;

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


       
        public async Task<CartResponseModel> GetById(Guid id)
        {
            var address = await _context.Carts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return address != null ? _mapper.Map<CartResponseModel>(address) : null;
        }


        public async Task<CartSearchResults> Search(CartSearchFilter filter)
        {
            if (_context.Carts == null)
                return new CartSearchResults { Items = new List<CartResponseModel>() };

            var query = _context.Carts.AsQueryable();

            //if (!string.IsNullOrEmpty(filter.CustomerId))
            //    query = query.Where(a => a.CustomerId.Contains(filter.CustomerId));
          

            var addresses = await query.ToListAsync();
            var results = _mapper.Map<List<CartResponseModel>>(addresses);

            return new CartSearchResults { Items = results };
        }

        public async Task<CartResponseModel> Create(CartCreateModel create)
        {
            var address = _mapper.Map<Cart>(create);
            address.CreatedAt = DateTime.UtcNow;
            address.UpdatedAt = DateTime.UtcNow;

            _context.Carts.Add(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<CartResponseModel>(address);
        }

        public async Task<CartResponseModel> Update(Guid id, CartUpdateModel update)
        {
            var address = await _context.Carts.FindAsync(id);
            if (address == null) return null;

            _mapper.Map(update, address);
            address.UpdatedAt = DateTime.UtcNow;

            _context.Carts.Update(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<CartResponseModel>(address);
        }

        public async Task<bool> Delete(Guid id)
        {
            var address = await _context.Carts.FindAsync(id);
            if (address == null) return false;

            _context.Carts.Remove(address);
            await _context.SaveChangesAsync();

            return true;
        }

    }


}
