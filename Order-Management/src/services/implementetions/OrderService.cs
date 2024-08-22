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

    public async Task<List<OrderResponseModel>> GetAll() =>

       _mapper.Map<List<OrderResponseModel>>(await _context.Orders.ToListAsync());



    public async Task<OrderResponseModel> GetById(Guid id)
    {
        var address = await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return address != null ? _mapper.Map<OrderResponseModel>(address) : null;
    }


    public async Task<OrderSearchResultsModel> Search(OrderSearchFilterModel filter)
    {
        if (_context.Orders == null)
            return new OrderSearchResultsModel { Items = new List<OrderResponseModel>() };

        var query = _context.Orders.AsQueryable();

        //if (!string.IsNullOrEmpty(filter.DisplayCode))
        //    query = query.Where(a => a.DisplayCode.Contains(filter.DisplayCode));


        var addresses = await query.ToListAsync();
        var results = _mapper.Map<List<OrderResponseModel>>(addresses);

        return new OrderSearchResultsModel { Items = results };
    }

    public async Task<OrderResponseModel> Create(OrderCreateModel create)
    {
        var address = _mapper.Map<Order>(create);
        address.CreatedAt = DateTime.UtcNow;
        address.UpdatedAt = DateTime.UtcNow;

        _context.Orders.Add(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderResponseModel>(address);
    }

    public async Task<OrderResponseModel> Update(Guid id, OrderUpdateModel update)
    {
        var address = await _context.Orders.FindAsync(id);
        if (address == null) return null;

        _mapper.Map(update, address);
        address.UpdatedAt = DateTime.UtcNow;

        _context.Orders.Update(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderResponseModel>(address);
    }

    public async Task<bool> Delete(Guid id)
    {
        var address = await _context.Orders.FindAsync(id);
        if (address == null) return false;

        _context.Orders.Remove(address);
        await _context.SaveChangesAsync();

        return true;
    }

}


