using order_management.domain_types.enums;
using Stateless;
namespace Order_Management.src.database.models
{
    public class OrderStatusMachine
    {

        public OrderStatusTypes State { get; internal set; } = OrderStatusTypes.DRAFT;

        public void CreateOrder()
        {
            if (State == OrderStatusTypes.DRAFT)
            {
                State = OrderStatusTypes.INVENTORY_CHECKED;
                

                Console.WriteLine("Order Created");
            }
        }

        public void ConfirmOrder()
        {
            if (State == OrderStatusTypes.INVENTORY_CHECKED)
            {
                State = OrderStatusTypes.CONFIRMED;
                Console.WriteLine("Order Confirmed");
            }
        }

        public void InitiatePayment()
        {
            if (State == OrderStatusTypes.CONFIRMED)
            {
               State = OrderStatusTypes.PAYMENT_INITIATED;
               
                Console.WriteLine("Payment is initiated");
            }
        }

        public void CompletePayment()
        {
            if (State == OrderStatusTypes.PAYMENT_INITIATED)
            {
                State = OrderStatusTypes.PAYMENT_COMPLETED;
                Console.WriteLine("Payment completed");
            }
        }

        public void RetryPayment()
        {
            if (State == OrderStatusTypes.PAYMENT_INITIATED)
            {
                State = OrderStatusTypes.PAYMENT_FAILED;
                Console.WriteLine("Payment failed");
            }
        }

        public void PlaceOrder()
        {
            if (State == OrderStatusTypes.PAYMENT_COMPLETED)
            {
                State = OrderStatusTypes.PLACED;
                Console.WriteLine("Order placed successfully");
            }
        }

        // Add remaining transitions similarly...

        public void CancelOrder()
        {
            if (State != OrderStatusTypes.CANCELLED)
            {
                State = OrderStatusTypes.CANCELLED;
                Console.WriteLine("Order is cancelled");
            }
        }

        public void ShipOrder()
        {
            if (State == OrderStatusTypes.PLACED)
            {
                State = OrderStatusTypes.SHIPPED;
                Console.WriteLine("Order shipped");
            }
        }

        public void DeliverOrder()
        {
            if (State == OrderStatusTypes.SHIPPED)
            {
                State = OrderStatusTypes.DELIVERED;
                Console.WriteLine("Order delivered successfully!");
            }
        }

        public void CloseOrder()
        {
            if (State == OrderStatusTypes.DELIVERED || State == OrderStatusTypes.REFUNDED || State == OrderStatusTypes.EXCHANGED)
            {
                State = OrderStatusTypes.CLOSED;
                Console.WriteLine("Order closed");
            }
        }

        public void ReopenOrder()
        {
            if (State == OrderStatusTypes.CLOSED)
            {
                State = OrderStatusTypes.REOPENED;
                Console.WriteLine("Order reopened");
            }
        }

        public void InitiateReturn()
        {
            if (State == OrderStatusTypes.REOPENED)
            {
                State = OrderStatusTypes.RETURN_INITIATED;
                Console.WriteLine("Return initiated");
            }
        }

        public void CompleteReturn()
        {
            if (State == OrderStatusTypes.RETURN_INITIATED)
            {
                State = OrderStatusTypes.RETURNED;
                Console.WriteLine("Return completed");
            }
        }

        public void InitiateRefund()
        {
            if (State == OrderStatusTypes.RETURNED)
            {
                State = OrderStatusTypes.REFUND_INITIATED;
                Console.WriteLine("Refund initiated");
            }
        }

        public void CompleteRefund()
        {
            if (State == OrderStatusTypes.REFUND_INITIATED)
            {
                State = OrderStatusTypes.REFUNDED;
                Console.WriteLine("Refund completed");
            }
        }

        public void InitiateExchange()
        {
            if (State == OrderStatusTypes.REOPENED)
            {
                State = OrderStatusTypes.EXCHANGE_INITIATED;
                Console.WriteLine("Exchange initiated");
            }
        }

        public void CompleteExchange()
        {
            if (State == OrderStatusTypes.EXCHANGE_INITIATED)
            {
                State = OrderStatusTypes.EXCHANGED;
                Console.WriteLine("Exchange completed");
            }
        }
    
}
}
