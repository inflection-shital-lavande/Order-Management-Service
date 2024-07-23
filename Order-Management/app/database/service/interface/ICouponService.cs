using Order_Management.app.domain_types.dto;
using Order_Management.app.domain_types.dto.couponDTO;
using Order_Management.Auth;

namespace Order_Management.app.database.service
{
    public interface ICouponService
    {
        Task<couponResponseDTO> GetCouponByIdAsync(Guid id);
        Task<List<couponResponseDTO>> GetAll();

        Task<Response> CreateCouponAsync(couponCreateDTO couponCreateDTO);
        Task<Response> UpdateCouponAsync(Guid id, couponUpdateDTO couponUpdateDTO);
        Task<Response> DeleteCouponAsync(Guid id);
        Task<List<couponSearchResultsDTO>> SearchCouponAsync(couponSearchFilterDTO filter);
    }
}



