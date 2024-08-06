/*using AutoMapper;
using Order_Management.database.models;
using Order_Management.database;
using Order_Management.src.database.dto;
using Order_Management.src.services.interfaces;
using Microsoft.EntityFrameworkCore;
using Order_Management.Auth;
using Microsoft.IdentityModel.Tokens;
using Order_Management.database.dto;

namespace Order_Management.src.services.implementetions
{
    public class OrderService :IOrderService

    {
        private readonly OrderManagementContext _context;
        private readonly IMapper _mapper;

        public OrderService(OrderManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderResponseDTO> GetOrderByIdAsync(Guid id) =>
            _mapper.Map<OrderResponseDTO>(await _context.Orders.FindAsync(id));

        public async Task<List<OrderResponseDTO>> GetAll() =>

           _mapper.Map<List<OrderResponseDTO>>(await _context.Orders.ToListAsync());


        public async Task<Response> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            _context.Orders.Add(_mapper.Map<Order>(orderCreateDTO));
            await _context.SaveChangesAsync();
            return new Response(true, "saved");
        }

        public async Task<Response> UpdateOrderAsync(Guid id, OrderUpdateDTO request)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                return new Response(false, "Order not found");
            }

            // Map updated values to the existing entity
            _mapper.Map(request, existingOrder);

            // Attempt to save changes
            try
            {
                _context.Orders.Update(existingOrder);
                await _context.SaveChangesAsync();
                return new Response(true, "Order updated successfully");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency exception
                return new Response(false, "The Order was updated or deleted by another user. Please reload and try again.");
            }
        }

        public async Task<Response> DeleteOrderAsync(Guid id)
        {
            _context.Orders.Remove(await _context.Orders.FindAsync(id));
            await _context.SaveChangesAsync();
            return new Response(true, "Delete");
        }

        public Task<List<OrderSearchResultsDTO>> SearchOrderAsync(OrderSearchFilterDTO filter)
        {
            throw new NotImplementedException();
        }


        /* public async Task<List<OrderSearchResultsDTO>> SearchOrderAsync(OrderSearchFilterDTO filter)
         {

             var query = _context.Orders.AsQueryable();

             if (!string.IsNullOrEmpty(filter.InvoiceNumber))
                 query = query.Where(a => a.InvoiceNumber.Contains(filter.InvoiceNumber));






             var results = await query
                 .Select(a => _mapper.Map<OrderSearchResultsDTO>(a))
                 .ToListAsync();

             return results;
         }
        
    }
}
*/