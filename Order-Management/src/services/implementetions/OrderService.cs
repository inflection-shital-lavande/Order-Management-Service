using AutoMapper;
using Stateless;
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
using Order_Management.src.database.models;
using Microsoft.AspNetCore.Http.HttpResults;
using order_management.common;

namespace order_management.src.services.implementetions;

public class OrderService : IOrderService

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

    public async Task<OrderResponseModel> UpdateOrderStatus(Guid orderId, OrderStatusTypes status)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
        // var order = _context.Set<Order>().FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            // return ApiResponse.NotFound("Failure", $"Order with id {orderId} not found");
            throw new Exception($"Order with id {orderId} not found");
        }

        var previousState = order.OrderStatus;//.ToString();
        var updatedState = status;//.ToString();

        // Check if the state transition is valid
        if (CheckValidTransition(previousState, updatedState))
        {
            order.OrderStatus = status;
            _context.Update(order);
            _context.SaveChanges();
            _context.Entry(order).Reload();
            return _mapper.Map<OrderResponseModel>(order);

        }
        else
        {
            throw new InvalidOperationException("Invalid state transition");
        }
    }

    public bool CheckValidTransition(OrderStatusTypes previousState, OrderStatusTypes updatedState)
    {
        var orderStatus = new OrderStatusMachine();

        if (previousState == OrderStatusTypes.DRAFT && updatedState == OrderStatusTypes.INVENTORY_CHECKED)
        {
            orderStatus.State = previousState;
            orderStatus.CreateOrder();
            return true;
        }
        else if (previousState == OrderStatusTypes.INVENTORY_CHECKED && updatedState == OrderStatusTypes.CONFIRMED)
        {
            orderStatus.State = previousState;
            orderStatus.ConfirmOrder();
            return true;
        }
        else if (previousState == OrderStatusTypes.CONFIRMED && updatedState == OrderStatusTypes.PAYMENT_INITIATED)
        {
            orderStatus.State = previousState;
            orderStatus.InitiatePayment();
            return true;
        }
        else if (previousState == OrderStatusTypes.PAYMENT_INITIATED && updatedState == OrderStatusTypes.PAYMENT_COMPLETED)
        {
            orderStatus.State = previousState;
            orderStatus.CompletePayment();
            return true;
        }
        else if (previousState == OrderStatusTypes.PAYMENT_INITIATED && updatedState == OrderStatusTypes.PAYMENT_FAILED)
        {
            orderStatus.State = previousState;
            orderStatus.RetryPayment();
            return true;
        }
        else if (previousState == OrderStatusTypes.PAYMENT_COMPLETED && updatedState == OrderStatusTypes.PLACED)
        {
            orderStatus.State = previousState;
            orderStatus.PlaceOrder();
            return true;
        }
        //cancelld
        else if (new[]
             {
                OrderStatusTypes.INVENTORY_CHECKED,
                OrderStatusTypes.CONFIRMED,
                OrderStatusTypes.PAYMENT_INITIATED,
                OrderStatusTypes.PAYMENT_COMPLETED,
                OrderStatusTypes.PLACED,
                OrderStatusTypes.SHIPPED
            }.Contains(previousState) && updatedState == OrderStatusTypes.CANCELLED)
        {
            orderStatus.State = previousState;
            orderStatus.CancelOrder();
            return true;
        }
        else if (previousState == OrderStatusTypes.PLACED && updatedState == OrderStatusTypes.SHIPPED)
        {
            orderStatus.State = previousState;
            orderStatus.ShipOrder();
            return true;
        }
        else if (previousState == OrderStatusTypes.SHIPPED && updatedState == OrderStatusTypes.DELIVERED)
        {
            orderStatus.State = previousState;
            orderStatus.DeliverOrder();
            return true;
        }
        // Logic to cancel the order from multiple states, including SHIPPING 
     
        else if (new[] { OrderStatusTypes.DELIVERED, OrderStatusTypes.EXCHANGED, OrderStatusTypes.REFUNDED }.Contains(previousState) && updatedState == OrderStatusTypes.CLOSED)
        {
            orderStatus.State = previousState;
            orderStatus.CloseOrder();
            return true;
        }
        else if (previousState == OrderStatusTypes.CLOSED && updatedState == OrderStatusTypes.REOPENED)
        {
            orderStatus.State = previousState;
            orderStatus.ReopenOrder();
            return true;
        }
        else if (previousState == OrderStatusTypes.REOPENED && updatedState == OrderStatusTypes.RETURN_INITIATED)
        {
            orderStatus.State = previousState;
            orderStatus.InitiateReturn();
            return true;
        }
        else if (previousState == OrderStatusTypes.RETURN_INITIATED && updatedState == OrderStatusTypes.RETURNED)
        {
            orderStatus.State = previousState;
            orderStatus.CompleteReturn();
            return true;
        }
        else if (previousState == OrderStatusTypes.RETURNED && updatedState == OrderStatusTypes.REFUND_INITIATED)
        {
            orderStatus.State = previousState;
            orderStatus.InitiateRefund();
            return true;
        }
        else if (previousState == OrderStatusTypes.REFUND_INITIATED && updatedState == OrderStatusTypes.REFUNDED)
        {
            orderStatus.State = previousState;
            orderStatus.CompleteRefund();
            return true;
        }
        else if (previousState == OrderStatusTypes.REOPENED && updatedState == OrderStatusTypes.EXCHANGE_INITIATED)
        {
            orderStatus.State = previousState;
            orderStatus.InitiateExchange();
            return true;
        }
        else if (previousState == OrderStatusTypes.EXCHANGE_INITIATED && updatedState == OrderStatusTypes.EXCHANGED)
        {
            orderStatus.State = previousState;
            orderStatus.CompleteExchange();
            return true;
        }

        return false;
    }
}


/*
public bool CheckValidTransition(OrderStatusTypes previousState, OrderStatusTypes updatedState)
 {
     var orderStatus = new OrderStatusMachine();

     if (previousState == OrderStatusTypes.DRAFT && updatedState == OrderStatusTypes.INVENTORY_CHECKED)
     {
         orderStatus.State = previousState;
         orderStatus.CreateOrder();
         return true;
     }
     else if (previousState == OrderStatusTypes.INVENTORY_CHECKED && updatedState == OrderStatusTypes.CONFIRMED)
     {
         orderStatus.State = previousState;
         orderStatus.ConfirmOrder();
         return true;
     }
     else if (previousState == OrderStatusTypes.CONFIRMED && updatedState == OrderStatusTypes.PAYMENT_INITIATED)
     {
         orderStatus.State = previousState;
         orderStatus.InitiatePayment();
         return true;
     }
     else if (previousState == OrderStatusTypes.PAYMENT_INITIATED && updatedState == OrderStatusTypes.PAYMENT_COMPLETED)
     {
         orderStatus.State = previousState;
         orderStatus.CompletePayment();
         return true;
     }
     else if (previousState == OrderStatusTypes.PAYMENT_INITIATED && updatedState == OrderStatusTypes.PAYMENT_FAILED)
     {
         orderStatus.State = previousState;
         orderStatus.RetryPayment();
         return true;
     }
     else if (previousState == OrderStatusTypes.PAYMENT_COMPLETED && updatedState == OrderStatusTypes.PLACED)
     {
         orderStatus.State = previousState;
         orderStatus.PlaceOrder();
         return true;
     }
     else if (previousState == OrderStatusTypes.PLACED && updatedState == OrderStatusTypes.SHIPPED)
     {
         orderStatus.State = previousState;
         orderStatus.ShipOrder();
         return true;
     }
     else if (previousState == OrderStatusTypes.SHIPPED && updatedState == OrderStatusTypes.DELIVERED)
     {
         orderStatus.State = previousState;
         orderStatus.DeliverOrder();
         return true;
     }
     else if (new[] { OrderStatusTypes.DELIVERED, OrderStatusTypes.EXCHANGED, OrderStatusTypes.REFUNDED }.Contains(previousState) && updatedState == OrderStatusTypes.CLOSED)
     {
         orderStatus.State = previousState;
         orderStatus.CloseOrder();
         return true;
     }
     else if (previousState == OrderStatusTypes.CLOSED && updatedState == OrderStatusTypes.REOPENED)
     {
         orderStatus.State = previousState;
         orderStatus.ReopenOrder();
         return true;
     }
     else if (previousState == OrderStatusTypes.REOPENED && updatedState == OrderStatusTypes.RETURN_INITIATED)
     {
         orderStatus.State = previousState;
         orderStatus.InitiateReturn();
         return true;
     }
     else if (previousState == OrderStatusTypes.RETURN_INITIATED && updatedState == OrderStatusTypes.RETURNED)
     {
         orderStatus.State = previousState;
         orderStatus.CompleteReturn();
         return true;
     }
     else if (previousState == OrderStatusTypes.RETURNED && updatedState == OrderStatusTypes.REFUND_INITIATED)
     {
         orderStatus.State = previousState;
         orderStatus.InitiateRefund();
         return true;
     }
     else if (previousState == OrderStatusTypes.REFUND_INITIATED && updatedState == OrderStatusTypes.REFUNDED)
     {
         orderStatus.State = previousState;
         orderStatus.CompleteRefund();
         return true;
     }
     else if (previousState == OrderStatusTypes.REOPENED && updatedState == OrderStatusTypes.EXCHANGE_INITIATED)
     {
         orderStatus.State = previousState;
         orderStatus.InitiateExchange();
         return true;
     }
     else if (previousState == OrderStatusTypes.EXCHANGE_INITIATED && updatedState == OrderStatusTypes.EXCHANGED)
     {
         orderStatus.State = previousState;
         orderStatus.CompleteExchange();
         return true;
     }

     return false;
 }
*/












