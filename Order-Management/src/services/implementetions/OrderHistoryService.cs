using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.merchant;
using order_management.src.database.dto.orderHistory;
using Microsoft.EntityFrameworkCore;
using order_management.src.services.interfaces;
using order_management.database.dto;
using order_management.domain_types.enums;

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

   
    public async Task<List<OrderHistoryResponseModel>> GetAll()
    {
        var OrderHistorys = await _context.OrderHistorys
            .Include(c => c.Order)

            .ToListAsync();

        return _mapper.Map<List<OrderHistoryResponseModel>>(OrderHistorys);
    }

    


    public async Task<OrderHistoryResponseModel> GetById(Guid id)
    {
        var OrderHistorys = await _context.OrderHistorys
            .AsNoTracking()
            .Include(c => c.Order)
            .FirstOrDefaultAsync(a => a.Id == id);

        return OrderHistorys != null ? _mapper.Map<OrderHistoryResponseModel>(OrderHistorys) : null;
    }




    public async Task<OrderHistorySearchResultsModel> Search(OrderHistorySearchFilterModel filter)
    {
        if (_context.OrderHistorys == null)
            return new OrderHistorySearchResultsModel { Items = new List<OrderHistoryResponseModel>() };

        var query = _context.OrderHistorys.AsQueryable();

        // Apply filters to the query
        if (filter.OrderId.HasValue)
            query = query.Where(oh => oh.OrderId == filter.OrderId.Value);

        if (filter.PreviousStatus != OrderStatusTypes.DRAFT)
            query = query.Where(oh => oh.PreviousStatus == filter.PreviousStatus);

        if (filter.Status != OrderStatusTypes.DRAFT)
            query = query.Where(oh => oh.Status == filter.Status);

        if (filter.UpdatedByUserId.HasValue)
            query = query.Where(oh => oh.UpdatedByUserId == filter.UpdatedByUserId.Value);

        if (filter.Timestamp.HasValue)
            query = query.Where(oh => oh.Timestamp == filter.Timestamp.Value);


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


