using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.merchant;
using order_management.src.database.dto.orderHistory;
using Microsoft.EntityFrameworkCore;
using order_management.src.services.interfaces;

namespace order_management.src.services.implementetions;

public class OrderHistoryService :IOrderHistoryService
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public OrderHistoryService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderHistoryResponseModel>> GetAll() =>

       _mapper.Map<List<OrderHistoryResponseModel>>(await _context.OrderHistorys.ToListAsync());



    public async Task<OrderHistoryResponseModel> GetById(Guid id)
    {
        var address = await _context.OrderHistorys
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return address != null ? _mapper.Map<OrderHistoryResponseModel>(address) : null;
    }


    public async Task<OrderHistorySearchResultsModel> Search(OrderHistorySearchFilterModel filter)
    {
        if (_context.OrderHistorys == null)
            return new OrderHistorySearchResultsModel { Items = new List<OrderHistoryResponseModel>() };

        var query = _context.OrderHistorys.AsQueryable();

        //if (!string.IsNullOrEmpty(filter.OrderId))
        //    query = query.Where(a => a.OrderId.Contains(filter.OrderId));


        var addresses = await query.ToListAsync();
        var results = _mapper.Map<List<OrderHistoryResponseModel>>(addresses);

        return new OrderHistorySearchResultsModel { Items = results };
    }

    public async Task<OrderHistoryResponseModel> Create(OrderHistoryCreateModel create)
    {
        var address = _mapper.Map<OrderHistory>(create);
        

        _context.OrderHistorys.Add(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderHistoryResponseModel>(address);
    }

    public async Task<OrderHistoryResponseModel> Update(Guid id, OrderHistoryUpdateModel update)
    {
        var address = await _context.OrderHistorys.FindAsync(id);
        if (address == null) return null;

        _mapper.Map(update, address);

        _context.OrderHistorys.Update(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderHistoryResponseModel>(address);
    }

    public async Task<bool> Delete(Guid id)
    {
        var address = await _context.OrderHistorys.FindAsync(id);
        if (address == null) return false;

        _context.OrderHistorys.Remove(address);
        await _context.SaveChangesAsync();

        return true;
    }

}


