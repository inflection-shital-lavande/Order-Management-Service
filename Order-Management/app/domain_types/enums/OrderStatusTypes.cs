﻿namespace Order_Management.app.domain_types.enums
{
    public enum OrderStatusTypes
    {
        DRAFT,
        InventoryChecked,
        Placed,
        Confirmed,
        PaymentInitiated,
        PaymentCompleted,
        PaymentFailed,
        Cancelled,
        Shipped,
        Delivered,
        Closed,
        Reopened,
        ReturnInitiated,
        Returned,
        RefundInitiated,
        Refunded,
        ExchangeInitiated,
        Exchanged
    }

}
