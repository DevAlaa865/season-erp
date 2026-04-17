using BranchERP.Application.DTOs.BranchDailyTarget;
using BranchERP.Application.DTOs.Common;

namespace BranchERP.Application.Interfaces
{
    public interface IBranchDailyTargetService
    {
        Task<ApiResponse<BranchDailyTargetHeaderDto>> GetByIdAsync(int id);

        Task<ApiResponse<IReadOnlyList<BranchDailyTargetHeaderDto>>> GetByBranchAndDateAsync(
            int branchId,
            DateTime date);

        Task<ApiResponse<BranchDailyTargetHeaderDto>> CreateAsync(BranchDailyTargetHeaderCreateUpdateDto model);

        Task<ApiResponse<BranchDailyTargetHeaderDto>> UpdateAsync(int id, BranchDailyTargetHeaderCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
