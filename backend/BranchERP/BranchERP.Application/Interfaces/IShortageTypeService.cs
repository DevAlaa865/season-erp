using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.ShortageType;

namespace BranchERP.Application.Interfaces
{
    public interface IShortageTypeService
    {
        Task<ApiResponse<IReadOnlyList<ShortageTypeDto>>> GetAllAsync();

        Task<ApiResponse<IReadOnlyList<ShortageTypeDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null);

        Task<ApiResponse<ShortageTypeDto>> GetByIdAsync(int id);

        Task<ApiResponse<ShortageTypeDto>> CreateAsync(ShortageTypeCreateUpdateDto model);

        Task<ApiResponse<ShortageTypeDto>> UpdateAsync(int id, ShortageTypeCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
