using AutoMapper;
using order_management.database.models;
using order_management.database;
using Order_Management.src.database.dto.merchant;
using order_management.src.database.dto.orderHistory;
using Order_Management.src.services.interfaces;
using Order_Management.src.database.dto.order_line_item;
using Microsoft.EntityFrameworkCore;
using order_management.database.dto;

namespace Order_Management.src.services.implementetions;

public class OrderLineItemService:IOrderLineItem
{
    private readonly OrderManagementContext _context;
    private readonly IMapper _mapper;

    public OrderLineItemService(OrderManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /* public async Task<List<OrderLineItemResponseModel>> GetAll() =>

        _mapper.Map<List<OrderLineItemResponseModel>>(await _context.OrderLineItems.ToListAsync());



     public async Task<OrderLineItemResponseModel> GetById(Guid id)
     {
         var address = await _context.OrderLineItems
             .AsNoTracking()
             .FirstOrDefaultAsync(a => a.Id == id);

         return address != null ? _mapper.Map<OrderLineItemResponseModel>(address) : null;
     }

     public async Task<List<OrderLineItemResponseModel>> GetAll()
    {
        var orderLineItems = await _context.OrderLineItems
            .Select(oli => new OrderLineItemResponseModel
            {
                Id = oli.Id,
                Name = oli.Name ?? "Default Name", // Handle null value for Name
                Quantity = oli.Quantity,
            })
            .ToListAsync();

        return orderLineItems;
    }
 */
    public async Task<List<OrderLineItemResponseModel>> GetAll()
    {
        // Fetch all OrderLineItems including related Cart and Order entities
        var orderLineItems = await _context.OrderLineItems
            .Include(oli => oli.Carts)  // Include related Cart entity
            //.Include(oli => oli.Orders) // Include related Order entity
            .ToListAsync();            // Execute the query and get the result as a list

        // Map the fetched entities to the response models
        return _mapper.Map<List<OrderLineItemResponseModel>>(orderLineItems);
    }


    public async Task<OrderLineItemResponseModel> GetById(Guid id)
    {
        var orderLineItem = await _context.OrderLineItems
             .Include(oli => oli.Carts) // Include the related Cart
            //.Include(oli => oli.Orders) // Include the related Order
            .FirstOrDefaultAsync(oli => oli.Id == id);

        return orderLineItem != null ? _mapper.Map<OrderLineItemResponseModel>(orderLineItem) : null;
    }

    public async Task<OrderLineItemSearchResults> Search(OrderLineItemSearchFilter filter)
    {
        if (_context.OrderLineItems == null)
            return new OrderLineItemSearchResults { Items = new List<OrderLineItemResponseModel>() };

        var query = _context.OrderLineItems.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(a => a.Name.Contains(filter.Name));


        var addresses = await query.ToListAsync();
        var results = _mapper.Map<List<OrderLineItemResponseModel>>(addresses);

        return new OrderLineItemSearchResults { Items = results };
    }

    public async Task<OrderLineItemResponseModel> Create(OrderLineItemCreateModel create)
    {
        var address = _mapper.Map<OrderLineItem>(create);
        address.CreatedAt = DateTime.UtcNow;
        address.UpdatedAt = DateTime.UtcNow;

        _context.OrderLineItems.Add(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderLineItemResponseModel>(address);
    }

    public async Task<OrderLineItemResponseModel> Update(Guid id, OrderLineItemUpdateModel update)
    {
        var address = await _context.OrderLineItems.FindAsync(id);
        if (address == null) return null;

        _mapper.Map(update, address);
        address.UpdatedAt = DateTime.UtcNow;

        _context.OrderLineItems.Update(address);
        await _context.SaveChangesAsync();

        return _mapper.Map<OrderLineItemResponseModel>(address);
    }

    public async Task<bool> Delete(Guid id)
    {
        var address = await _context.OrderLineItems.FindAsync(id);
        if (address == null) return false;

        _context.OrderLineItems.Remove(address);
        await _context.SaveChangesAsync();

        return true;
    }

}
