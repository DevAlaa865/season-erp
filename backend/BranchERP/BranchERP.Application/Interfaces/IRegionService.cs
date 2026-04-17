using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.Region;

namespace BranchERP.Application.Interfaces
{
    public interface IRegionService
    {
        Task<ApiResponse<IReadOnlyList<RegionDto>>> GetAllAsync();

        Task<ApiResponse<IReadOnlyList<RegionDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null);

        Task<ApiResponse<RegionDto>> GetByIdAsync(int id);

        Task<ApiResponse<RegionDto>> CreateAsync(RegionCreateUpdateDto model);

        Task<ApiResponse<RegionDto>> UpdateAsync(int id, RegionCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
