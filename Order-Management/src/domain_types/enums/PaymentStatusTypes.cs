using System.Text.Json.Serialization;

namespace order_management.domain_types.enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentStatusTypes
{
    INITIATED ,
    INPROGRESS,
    SUCCEEDED ,
    FAILED ,
    CANCELLED,
    REFUNDED,
    EXPIRED,
    UNKNOWN
}
