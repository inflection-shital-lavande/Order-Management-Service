using Order_Management.Auth;
using Order_Management.database.dto;

namespace Order_Management.services.interfaces
{
    public interface ICouponService
    {

        Task<CouponResponseDTO> GetCouponByIdAsync(Guid id);
        Task<List<CouponResponseDTO>> GetAll();

        Task<Response> CreateCouponAsync(CouponCreateDTO couponCreateDTO);
        Task<Response> UpdateCouponAsync(Guid id, CouponUpdateDTO couponUpdateDTO);
        Task<Response> DeleteCouponAsync(Guid id);
        Task<List<CouponSearchResultsDTO>> SearchCouponAsync(CouponSearchFilterDTO filter);
    }
}
