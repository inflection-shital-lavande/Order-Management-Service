using System.Text.Json.Serialization;

namespace order_management.domain_types.enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DiscountTypes
{
    FLAT,
    PERCENTAGE

}
