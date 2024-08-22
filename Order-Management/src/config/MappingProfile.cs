using AutoMapper;

using order_management.auth;
using order_management.database.dto;
using order_management.database.models;
using order_management.src.database.dto;
using order_management.src.database.dto.orderHistory;
using Order_Management.src.database.dto.cart;
using Order_Management.src.database.dto.fileUpload;
using Order_Management.src.database.dto.merchant;
using Order_Management.src.database.dto.order_line_item;
using Order_Management.src.database.dto.orderType;
using Order_Management.src.database.dto.payment_transaction;
using Order_Management.src.database.models;

namespace order_management.config;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressCreateModel>().ReverseMap();
        CreateMap<Address, AddressUpdateModel>().ReverseMap();
        CreateMap<Address, AddressResponseModel>().ReverseMap();
        CreateMap<Address, AddressSearchFilterModel>().ReverseMap();
        CreateMap<Address, AddressSearchResultsModel>().ReverseMap();

        //cart
        CreateMap<Cart, CartCreateModel>().ReverseMap();
        CreateMap<Cart, CartUpdateModel>().ReverseMap();
        CreateMap<Cart, CartResponseModel>().ReverseMap();
        CreateMap<Cart, CartSearchFilter>().ReverseMap();
        CreateMap<Cart, CartSearchResults>().ReverseMap();


        CreateMap<Coupon, CouponCreateModel>().ReverseMap();
        CreateMap<Coupon, CouponUpdateModel>().ReverseMap();
        CreateMap<Coupon, CouponResponseModel>().ReverseMap();
        CreateMap<Coupon, CouponSearchFilterModel>().ReverseMap();
        CreateMap<Coupon, CouponSearchResultsModel>().ReverseMap();


        CreateMap<Customer, CustomerCreateModel>().ReverseMap();
        CreateMap<Customer, CustomerUpdateModel>().ReverseMap();
        CreateMap<Customer, CustomerResponseModel>().ReverseMap();
        CreateMap<Customer, CustomerSearchFilterModel>().ReverseMap();
        CreateMap<Customer, CustomerSearchResultsModel>().ReverseMap();

        CreateMap<CustomerAddress, CustomerAddressCreateDTO>().ReverseMap();

        //Merchant
        CreateMap<Merchant, MerchantCreateModel>().ReverseMap();
        CreateMap<Merchant, MerchantUpdateModel>().ReverseMap();
        CreateMap<Merchant, MerchantResponseModel>().ReverseMap();
        CreateMap<Merchant, MerchantSearchFilter>().ReverseMap();
        CreateMap<Merchant, MerchantSearchResults>().ReverseMap();

        //Order

        CreateMap<Order, OrderCreateModel>().ReverseMap();
        CreateMap<Order, OrderUpdateModel>().ReverseMap();
        CreateMap<Order, OrderResponseModel>().ReverseMap();
        CreateMap<Order, OrderSearchFilterModel>().ReverseMap();
        CreateMap<Order, OrderSearchResultsModel>().ReverseMap();

        //OrderHistory
        CreateMap<OrderHistory, OrderHistoryCreateModel>().ReverseMap();
        CreateMap<OrderHistory, OrderHistoryUpdateModel>().ReverseMap();
        CreateMap<OrderHistory, OrderHistoryResponseModel>().ReverseMap();
        CreateMap<OrderHistory, OrderHistorySearchFilterModel>().ReverseMap();
        CreateMap<OrderHistory, OrderHistorySearchResultsModel>().ReverseMap();

        //OrderLineItem
        CreateMap<OrderLineItem, OrderLineItemCreateModel>().ReverseMap();
        CreateMap<OrderLineItem, OrderLineItemUpdateModel>().ReverseMap();
        CreateMap<OrderLineItem, OrderLineItemResponseModel>().ReverseMap();
        CreateMap<OrderLineItem, OrderLineItemSearchFilter>().ReverseMap();
        CreateMap<OrderLineItem, OrderLineItemSearchResults>().ReverseMap();

        //OrderType
        CreateMap<OrderType, OrderTypeCreateModel>().ReverseMap();
        CreateMap<OrderType, OrderTypeUpdateModel>().ReverseMap();
        CreateMap<OrderType, OrderTypeSearchFilter>().ReverseMap();
        CreateMap<OrderType, OrderTypeResponseModel>().ReverseMap();
        CreateMap<OrderType, OrderTypeSearchResults>().ReverseMap();

        //OrderType
        CreateMap<OrderType, OrderTypeCreateModel>().ReverseMap();
        CreateMap<OrderType, OrderTypeUpdateModel>().ReverseMap();
        CreateMap<OrderType, OrderTypeSearchFilter>().ReverseMap();
        CreateMap<OrderType, OrderTypeResponseModel>().ReverseMap();
        CreateMap<OrderType, OrderTypeSearchResults>().ReverseMap();
        //PaymentTransaction
        CreateMap<PaymentTransaction, PaymentTransactionCreateModel>().ReverseMap();
        CreateMap<PaymentTransaction, PaymentTransactionResponseModel>().ReverseMap();
        CreateMap<PaymentTransaction, PaymentTransactionSearchFilter>().ReverseMap();
        CreateMap<PaymentTransaction, PaymentTransactionSearchResults>().ReverseMap();

        CreateMap<CustomerAddressCreateDTO, CustomerAddress>().ReverseMap();


        //authentication
        CreateMap<RegisterDTO, User>();

        CreateMap<FileUploadDTO,FileMetadata >();

    }
}
