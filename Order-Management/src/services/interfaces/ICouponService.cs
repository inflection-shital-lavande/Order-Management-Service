using order_management.auth;
using order_management.database.dto;
using order_management.database.models;
using Order_Management.src.database.dto.cart;

namespace order_management.services.interfaces;

public interface ICouponService
{

    Task<List<CouponResponseModel>> GetAll();
    // Task<IEnumerable<Coupon>> GetAll();

    Task<CouponResponseModel> GetById(Guid id);
    Task<CouponResponseModel> Create(CouponCreateModel couponCreate);
    Task<CouponResponseModel> Update(Guid id, CouponUpdateModel couponUpdate);
    Task<bool> Delete(Guid id);
    Task<CouponSearchResultsModel> Search(CouponSearchFilterModel filter);

}


