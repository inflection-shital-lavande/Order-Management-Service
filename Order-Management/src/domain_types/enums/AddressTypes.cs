using System.Text.Json.Serialization;

namespace order_management.domain_types.enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AddressTypes
{
    WORK,
    HOME, 
    SHIPPING,
    BILLING,
    UNSPECIFIED
   
}
