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
            .Include(o => o.Cart)
            .Include(o => o.Customer)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress)
            .Include(o => o.OrderType)
            .Include(o => o.OrderHistory)
            .ToListAsync();

        return _mapper.Map<List<OrderResponseModel>>(orders);
    }


    public async Task<OrderResponseModel> GetById(Guid id)
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(c => c.Cart)
            .Include(c => c.Customer)
            .Include(c => c.ShippingAddress)
            .Include(c => c.BillingAddress)
            .Include(c => c.OrderType)
            .Include(c => c.OrderHistory)
            .FirstOrDefaultAsync(a => a.Id == id);

        return orders != null ? _mapper.Map<OrderResponseModel>(orders) : null;
    }


    public async Task<OrderSearchResultsModel> Search(OrderSearchFilterModel filter)
    {
        if (_context.Orders == null)
            return new OrderSearchResultsModel { Items = new List<OrderResponseModel>() };

        var query = _context.Orders.AsQueryable();

        //if (!string.IsNullOrEmpty(filter.DisplayCode))
        //    query = query.Where(a => a.DisplayCode.Contains(filter.DisplayCode));


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


