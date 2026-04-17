using BranchERP.Application.DTOs.BranchSalesDaily;
using BranchERP.Application.DTOs.Common;

namespace BranchERP.Application.Interfaces
{
    public interface IBranchSalesDailyService
    {
        Task<ApiResponse<BranchSalesDailyDto>> GetByIdAsync(int id);

        Task<ApiResponse<IReadOnlyList<BranchSalesDailyDto>>> GetByBranchAndDateAsync(
            int branchId,
            DateTime date);

        Task<ApiResponse<BranchSalesDailyDto>> CreateAsync(BranchSalesDailyCreateUpdateDto model);
        Task<ApiResponse<BranchSalesDailyDto>> UpdateAsync(int id, BranchSalesDailyCreateUpdateDto model);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        public  Task<bool> ExistsAsync(int branchId, DateTime date);
        Task<ApiResponse<IReadOnlyList<BranchDailySummaryRowDto>>> GetSummaryReportAsync(BranchDailySummaryFilterDto filter);

    }
}
