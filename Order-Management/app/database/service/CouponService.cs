using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Order_Management.app.database.models;
using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.couponDTO;
using Order_Management.Auth;
using Order_Management.Data;

namespace Order_Management.app.database.service
{
    public class CouponService : ICouponService
    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public CouponService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<Response> CreateCouponAsync(couponCreateDTO couponCreateDTO)
        {
            _context.Coupons.Add(_mapper.Map<Coupon>(couponCreateDTO));
            await _context.SaveChangesAsync();
            return new Response(true, "saved");
        }
        public async Task<Response> DeleteCouponAsync(Guid id)
        {
            _context.Coupons.Remove(await _context.Coupons.FindAsync(id));
            await _context.SaveChangesAsync();
            return new Response(true, "Delete");
        }

      
        public async Task<couponResponseDTO> GetCouponByIdAsync(Guid id) =>
           _mapper.Map<couponResponseDTO>(await _context.Coupons.FindAsync(id));

        public async Task<List<couponResponseDTO>> GetAll() =>

           _mapper.Map<List<couponResponseDTO>>(await _context.Coupons.ToListAsync());


       
        public async Task<List<couponSearchResultsDTO>> SearchCouponAsync(couponSearchFilterDTO filter)
        {

            var query = _context.Coupons.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(a => a.Name.Contains(filter.Name));

           

            var results = await query
                .Select(a => _mapper.Map<couponSearchResultsDTO>(a))
                .ToListAsync();

            return results;
        }

        public async Task<Response> UpdateCouponAsync(Guid id, couponUpdateDTO couponUpdateDTO)
        {
            var existingCoupon = await _context.Coupons.FindAsync(id);
            if (existingCoupon == null)
            {
                return new Response(false, "Coupon not found");
            }

            // Map updated values to the existing entity
            _mapper.Map(couponUpdateDTO, existingCoupon);

            // Attempt to save changes
            try
            {
                _context.Coupons.Update(existingCoupon);
                await _context.SaveChangesAsync();
                return new Response(true, "Coupon updated successfully");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exception
                return new Response(false, "The coupon was updated or deleted by another user. Please reload and try again.");
            }
        }

    }
}

