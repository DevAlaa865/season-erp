using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.City;

namespace BranchERP.Application.Interfaces
{
    public interface ICityService
    {
        Task<ApiResponse<IReadOnlyList<CityDto>>> GetAllAsync();

        Task<ApiResponse<IReadOnlyList<CityDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null);

        Task<ApiResponse<CityDto>> GetByIdAsync(int id);

        Task<ApiResponse<CityDto>> CreateAsync(CityCreateUpdateDto model);

        Task<ApiResponse<CityDto>> UpdateAsync(int id, CityCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
