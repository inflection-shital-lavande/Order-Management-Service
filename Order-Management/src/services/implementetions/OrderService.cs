using AutoMapper;
using order_management.database.models;
using order_management.database;
using order_management.src.database.dto;
using order_management.src.services.interfaces;
using Microsoft.EntityFrameworkCore;
using order_management.auth;
using Microsoft.IdentityModel.Tokens;
using order_management.database.dto;
using Order_Management.src.database.dto.merchant;
using order_management.src.database.dto.orderHistory;
using order_management.domain_types.enums;

namespace order_management.src.services.implementetions;

public class OrderService :IOrderService

{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public OrderService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderResponseModel>> GetAll()
    {
        var orders = await _context.Orders
            .Include(o => o.Carts)
            .Include(o => o.Customers)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress)
            .Include(o => o.OrderTypes)
            .Include(o => o.OrderHistorys)
            .ToListAsync();

        return _mapper.Map<List<OrderResponseModel>>(orders);
    }
    
    //public async Task<List<OrderResponseModel>> GetAll()
    //{
    //    return await _context.Orders
            
    //        .Select(a => _mapper.Map<OrderResponseModel>(a))
    //        .ToListAsync();
    //}

    public async Task<OrderResponseModel> GetById(Guid id)
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(c => c.Carts)
            .Include(c => c.Customers)
            .Include(c => c.ShippingAddress)
            .Include(c => c.BillingAddress)
            .Include(c => c.OrderTypes)
            .Include(c => c.OrderHistorys)
            .FirstOrDefaultAsync(a => a.Id == id);

        return orders != null ? _mapper.Map<OrderResponseModel>(orders) : null;
    }


    public async Task<OrderSearchResultsModel> Search(OrderSearchFilterModel filter)
    {
        if (_context.Orders == null)
            return new OrderSearchResultsModel { Items = new List<OrderResponseModel>() };

        var query = _context.Orders.AsQueryable();

        // Apply filters to the query
        if (filter.CustomerId.HasValue)
            query = query.Where(o => o.CustomerId == filter.CustomerId.Value);

        if (filter.AssociatedCartId.HasValue)
            query = query.Where(o => o.AssociatedCartId == filter.AssociatedCartId.Value);

        //if (filter.CouponId.HasValue)
        //    query = query.Where(o => o.CouponId == filter.CouponId.Value);

        if (filter.TotalItemsCountGreaterThan.HasValue)
            query = query.Where(o => o.TotalItemsCount > filter.TotalItemsCountGreaterThan.Value);

        if (filter.TotalItemsCountLessThan.HasValue)
            query = query.Where(o => o.TotalItemsCount < filter.TotalItemsCountLessThan.Value);

        if (filter.OrderDiscountGreaterThan.HasValue)
            query = query.Where(o => o.OrderDiscount > filter.OrderDiscountGreaterThan.Value);

        if (filter.OrderDiscountLessThan.HasValue)
            query = query.Where(o => o.OrderDiscount < filter.OrderDiscountLessThan.Value);

        if (filter.TipApplicable.HasValue)
            query = query.Where(o => o.TipApplicable == filter.TipApplicable.Value);

        if (filter.TotalAmountGreaterThan.HasValue)
            query = query.Where(o => o.TotalAmount > filter.TotalAmountGreaterThan.Value);

        if (filter.TotalAmountLessThan.HasValue)
            query = query.Where(o => o.TotalAmount < filter.TotalAmountLessThan.Value);

        //if (filter.OrderLineItemProductId.HasValue)
        //    query = query.Where(o => o.OrderLineItems.Any(oli => oli.ProductId == filter.OrderLineItemProductId.Value));

        //if (filter.OrderStatus != OrderStatusTypes.None)  // Assuming OrderStatusTypes.None represents no filter
        //    query = query.Where(o => o.OrderStatus == filter.OrderStatus);

        if (filter.OrderTypeId.HasValue)
            query = query.Where(o => o.OrderTypeId == filter.OrderTypeId.Value);

        //if (filter.CreatedBefore.HasValue)
        //    query = query.Where(o => o.CreatedDate < filter.CreatedBefore.Value);

        //if (filter.CreatedAfter.HasValue)
        //    query = query.Where(o => o.CreatedDate > filter.CreatedAfter.Value);

        //if (filter.PastMonths.HasValue && filter.PastMonths > 0)
        //{
        //    var pastDate = DateTime.UtcNow.AddMonths(-filter.PastMonths.Value);
        //    query = query.Where(o => o.CreatedDate >= pastDate);
        //}


        var orders = await query.ToListAsync();
        var results = _mapper.Map<List<OrderResponseModel>>(orders);

        return new OrderSearchResultsModel { Items = results };
    }

    public async Task<OrderResponseModel> Create(OrderCreateModel create)
    {
        var order = _mapper.Map<Order>(create);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderResponseModel>(order);

    }

    public async Task<OrderResponseModel> Update(Guid id, OrderUpdateModel update)
    {
        var orders = await _context.Orders.FindAsync(id);
        if (orders == null) return null;

        _mapper.Map(update, orders);
        orders.UpdatedAt = DateTime.UtcNow;

        _context.Orders.Update(orders);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderResponseModel>(orders);
    }

    public async Task<bool> Delete(Guid id)
    {
        var orders = await _context.Orders.FindAsync(id);
        if (orders == null) return false;

        _context.Orders.Remove(orders);
        await _context.SaveChangesAsync();

        return true;
    }

}


