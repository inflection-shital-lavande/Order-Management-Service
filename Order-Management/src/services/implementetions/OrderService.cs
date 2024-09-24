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

    public async Task<OrderResponseModel> UpdateOrderStatus(Guid orderId, OrderStatusTypes status)
    {
        // var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
        var order = _context.Set<Order>().FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
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
            //  return new OrderResponseModel(order); // Assuming OrderResponseModel constructor accepts an Order object
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
}
   /*  public OrderResponseModel UpdateOrderStatus(Guid orderId, OrderStatusTypes status)
      {
          var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
          if (order == null)
          {
              throw new Exception($"Order with id {orderId} not found");
          }

          var previousState = order.OrderStatus;
          var updatedState = status;

          if (CheckValidTransition(previousState, updatedState))
          {
              order.OrderStatus = updatedState;
              order.UpdatedAt = DateTime.Now;

              _context.Orders.Update(order);
              _context.SaveChanges();

              return _mapper.Map<OrderResponseModel>(order);
              /* return new OrderResponseModel
               {
                   Id = order.Id,
                 //  OrderStatus = order.OrderStatus//.ToString(),
                   // Other fields you want to include in the response
             //  };
}
        else
        {
            throw new InvalidOperationException("Invalid state transition");
        }
    }*/
/* public bool CheckValidTransition(string previousState,string updatedState)
 {
     var orderStatus = new OrderStatusMachine();

     if (previousState == "Draft" && updatedState == "Inventory Checked")
     {
         orderStatus.State = previousState;
         orderStatus.CreateOrder();
         return true;
     }
     else if (previousState == "Inventory Checked" && updatedState == "Confirmed")
     {
         orderStatus.State = previousState;
         orderStatus.ConfirmOrder();
         return true;
     }
     else if (previousState == "Confirmed" && updatedState == "Payment Initiated")
     {
         orderStatus.State = previousState;
         orderStatus.InitiatePayment();
         return true;
     }
     else if (previousState == "Payment Initiated" && updatedState == "Payment Completed")
     {
         orderStatus.State = previousState;
         orderStatus.CompletePayment();
         return true;
     }
     else if (previousState == "Payment Initiated" && updatedState == "Payment Failed")
     {
         orderStatus.State = previousState;
         orderStatus.RetryPayment();
         return true;
     }
     else if (previousState == "Payment Completed" && updatedState == "Placed")
     {
         orderStatus.State = previousState;
         orderStatus.PlaceOrder();
         return true;
     }
     else if (previousState == "Placed" && updatedState == "Shipped")
     {
         orderStatus.State = previousState;
         orderStatus.ShipOrder();
         return true;
     }
     else if (previousState == "Shipped" && updatedState == "Delivered")
     {
         orderStatus.State = previousState;
         orderStatus.DeliverOrder();
         return true;
     }
     else if (new[] { "Delivered", "Exchanged", "Refunded" }.Contains(previousState) && updatedState == "Closed")
     {
         orderStatus.State = previousState;
         orderStatus.CloseOrder();
         return true;
     }
     else if (previousState == "Closed" && updatedState == "Reopened")
     {
         orderStatus.State = previousState;
         orderStatus.ReopenOrder();
         return true;
     }
     else if (previousState == "Reopened" && updatedState == "Return Initiated")
     {
         orderStatus.State = previousState;
         orderStatus.InitiateReturn();
         return true;
     }
     else if (previousState == "Return Initiated" && updatedState == "Returned")
     {
         orderStatus.State = previousState;
         orderStatus.CompleteReturn();
         return true;
     }
     else if (previousState == "Returned" && updatedState == "Refund Initiated")
     {
         orderStatus.State = previousState;
         orderStatus.InitiateRefund();
         return true;
     }
     else if (previousState == "Refund Initiated" && updatedState == "Refunded")
     {
         orderStatus.State = previousState;
         orderStatus.CompleteRefund();
         return true;
     }
     else if (previousState == "Reopened" && updatedState == "Exchange Initiated")
     {
         orderStatus.State = previousState;
         orderStatus.InitiateExchange();
         return true;
     }
     else if (previousState == "Exchange Initiated" && updatedState == "Exchanged")
     {
         orderStatus.State = previousState;
         orderStatus.CompleteExchange();
         return true;
     }

     return false;
 }
}*/




//public class OrderStatus
//  {
//public bool CheckValidTransition(OrderStatusTypes previousState, OrderStatusTypes updatedState)
/*public bool CheckValidTransition(OrderStatusTypes previousState, string updatedState)//, OrderStatusMachine orderStatus)
     {
         var orderStatus = new OrderStatusMachine();


         switch (previousState)
         {
             case "Draft":
                 if (updatedState == "Inventry Checked")
                 {
                 orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("CreateOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Inventry Checked":
                 if (updatedState == "Confirmed")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("ConfirmOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Confirmed":
                 if (updatedState == "Payment Initiated")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("InitiatePayment");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Payment Initiated":
                 if (updatedState == "Payment Completed")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("CompletePayment");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 else if (updatedState == "Payment Failed")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("RetryPayment");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Payment Completed":
                 if (updatedState == "Placed")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("PlacedOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Placed":
                 if (updatedState == "Shipped")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("ShippedOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Shipped":
                 if (updatedState == "Delivered")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("DeliveredOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Delivered":
             case "Exchanged":
             case "Refunded":
                 if (updatedState == "Closed")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("ClosedOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Closed":
                 if (updatedState == "Reopened")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("ReopenOrder");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Reopened":
                 if (updatedState == "Return Initiated")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("InitiateReturn");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 else if (updatedState == "Exchange Initiated")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("InitiateExchange");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Return Initiated":
                 if (updatedState == "Returned")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("CompleteReturn");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Returned":
                 if (updatedState == "Refund Initiated")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("InitiateRefund");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Refund Initiated":
                 if (updatedState == "Refunded")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("CompletedRefund");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             case "Exchange Initiated":
                 if (updatedState == "Exchanged")
                 {
                     orderStatus.State = previousState;
                     var transitionMethod = orderStatus.GetType().GetMethod("CompleteExchange");
                     if (transitionMethod != null)
                     {
                         transitionMethod.Invoke(orderStatus, null);
                         return true;
                     }
                 }
                 break;

             default:
                 return false;
         }

         return false;
     }

 }*/




