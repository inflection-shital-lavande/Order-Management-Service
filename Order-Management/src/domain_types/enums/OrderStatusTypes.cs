using System.ComponentModel;
using System.Text.Json.Serialization;

namespace order_management.domain_types.enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatusTypes
{
  

    DRAFT ,
    INVENTORY_CHECKED,
    CONFIRMED,
    PAYMENT_INITIATED,
    PAYMENT_COMPLETED,
    PLACED,
    PAYMENT_FAILED,
    CANCELLED,
    SHIPPED,
    DELIVERED,
    CLOSED,
    REOPENED,
    RETURN_INITIATED,
    RETURNED,
    REFUND_INITIATED,
    REFUNDED,
    EXCHANGE_INITIATED,
    EXCHANGED
   
   
   

}














