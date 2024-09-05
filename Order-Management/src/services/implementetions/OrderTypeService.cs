using AutoMapper;
using Microsoft.EntityFrameworkCore;
using order_management.database;
using order_management.database.models;
using order_management.src.database.dto;
using Order_Management.src.database.dto.orderType;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.services.implementetions;

public class OrderTypeService : IOrderTypeService
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public OrderTypeService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderTypeResponseModel> GetById(Guid id)
    {
        var orderType = await _context.OrderTypes.FindAsync(id);
        return _mapper.Map<OrderTypeResponseModel>(orderType);
    }

    //public async Task<IEnumerable<OrderTypeResponseModel>> GetAll()
    //{
    //    var orderTypes = await _context.OrderTypes.ToListAsync();
    //    return _mapper.Map<IEnumerable<OrderTypeResponseModel>>(orderTypes);
    //}

    public async Task<List<OrderTypeResponseModel>> GetAll() =>

       _mapper.Map<List<OrderTypeResponseModel>>(await _context.OrderTypes.ToListAsync());

    public async Task<OrderTypeResponseModel> Create(OrderTypeCreateModel dto)
    {
        var orderType = _mapper.Map<OrderType>(dto);
        orderType.CreatedAt = DateTime.UtcNow;
        _context.OrderTypes.Add(orderType);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderTypeResponseModel>(orderType);
    }


    public async Task<OrderTypeSearchResults> Search(OrderTypeSearchFilter filter)
    {
        if (_context.Merchants == null)
            return new OrderTypeSearchResults { Items = new List<OrderTypeResponseModel>() };

        var query = _context.OrderTypes.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
           query = query.Where(a => a.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(a => a.Description.Contains(filter.Description));


        var addresses = await query.ToListAsync();
        var results = _mapper.Map<List<OrderTypeResponseModel>>(addresses);

        return new OrderTypeSearchResults { Items = results };
    }

    public async Task<OrderTypeResponseModel> Update(Guid id, OrderTypeUpdateModel dto)
    {
        var orderType = await _context.OrderTypes.FindAsync(id);
        if (orderType == null) return null;

        _mapper.Map(dto, orderType);
        orderType.UpdatedAt = DateTime.UtcNow;
        _context.OrderTypes.Update(orderType);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderTypeResponseModel>(orderType);
    }


    public async Task<bool> Delete(Guid id)
    {
        var orderType = await _context.OrderTypes.FindAsync(id);
        if (orderType == null) return false;

        _context.OrderTypes.Remove(orderType);
        await _context.SaveChangesAsync();
        return true;
    }
}
