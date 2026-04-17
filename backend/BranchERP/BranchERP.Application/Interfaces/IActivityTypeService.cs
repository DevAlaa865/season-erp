using BranchERP.Application.DTOs.ActivityType;
using BranchERP.Application.DTOs.Common;

namespace BranchERP.Application.Interfaces
{
    public interface IActivityTypeService
    {
        Task<ApiResponse<IReadOnlyList<ActivityTypeDto>>> GetAllAsync();

        Task<ApiResponse<IReadOnlyList<ActivityTypeDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null);

        Task<ApiResponse<ActivityTypeDto>> GetByIdAsync(int id);

        Task<ApiResponse<ActivityTypeDto>> CreateAsync(ActivityTypeCreateUpdateDto model);

        Task<ApiResponse<ActivityTypeDto>> UpdateAsync(int id, ActivityTypeCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
