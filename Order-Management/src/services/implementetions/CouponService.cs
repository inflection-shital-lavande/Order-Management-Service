using AutoMapper;
using Microsoft.EntityFrameworkCore;

using order_management.auth;
using order_management.database;
using order_management.database.dto;
using order_management.database.models;
using order_management.domain_types.enums;
using order_management.services.interfaces;
using Order_Management.src.database.dto.cart;


namespace order_management.services.implementetions;

public class CouponService : ICouponService
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public CouponService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


   
    public async Task<List<CouponResponseModel>> GetAll() =>

       _mapper.Map<List<CouponResponseModel>>(await _context.Coupons.ToListAsync());


    public async Task<CouponResponseModel> GetById(Guid id)
    {
        var coupons = await _context.Coupons
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return coupons != null ? _mapper.Map<CouponResponseModel>(coupons) : null;
    }


    public async Task<CouponSearchResultsModel> Search(CouponSearchFilterModel filter)
    {
        
        if (_context.Coupons == null)
            return new CouponSearchResultsModel { Items = new List<CouponResponseModel>() };

        
        var query = _context.Coupons.AsQueryable();


        // Apply filters to the query
        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(c => c.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.CouponCode))
            query = query.Where(c => c.CouponCode.Contains(filter.CouponCode));

        //if (filter.StartDate.HasValue)
        //    query = query.Where(c => c.StartDate.Date == filter.StartDate.Value.Date);

        if (filter.Discount.HasValue && filter.Discount.Value > 0)
            query = query.Where(c => c.Discount == filter.Discount.Value);

        if (filter.DiscountType != DiscountTypes.FLAT)
            query = query.Where(c => c.DiscountType == filter.DiscountType);

        if (filter.DiscountPercentage.HasValue && filter.DiscountPercentage.Value > 0)
            query = query.Where(c => c.DiscountPercentage == filter.DiscountPercentage.Value);

        if (filter.MinOrderAmount.HasValue && filter.MinOrderAmount.Value > 0)
            query = query.Where(c => c.MinOrderAmount >= filter.MinOrderAmount.Value);

        if (filter.IsActive.HasValue)
            query = query.Where(c => c.IsActive == filter.IsActive.Value);


        var coupons = await query.ToListAsync();

       
        var results = _mapper.Map<List<CouponResponseModel>>(coupons);

        
        return new CouponSearchResultsModel { Items = results };
    }







    public async Task<CouponResponseModel> Create(CouponCreateModel couponCreate)
    {
        var coupons = _mapper.Map<Coupon>(couponCreate);
        coupons.CreatedAt = DateTime.UtcNow;
        coupons.UpdatedAt = DateTime.UtcNow;

        _context.Coupons.Add(coupons);
        await _context.SaveChangesAsync();

        return _mapper.Map<CouponResponseModel>(coupons);
    }


   
   

    public async Task<CouponResponseModel> Update(Guid id, CouponUpdateModel update)
    {
        var existingCoupon = await _context.Coupons.FindAsync(id);
        if (existingCoupon == null) return null;

        _mapper.Map(update, existingCoupon);
        existingCoupon.UpdatedAt = DateTime.UtcNow;

        _context.Coupons.Update(existingCoupon);
        await _context.SaveChangesAsync();

        return _mapper.Map<CouponResponseModel>(existingCoupon);
    }


    public async Task<bool> Delete(Guid id)
    {
        var coupons = await _context.Coupons.FindAsync(id);
        if (coupons == null) return false;

        _context.Coupons.Remove(coupons);
        await _context.SaveChangesAsync();

        return true;
    }
   

}

