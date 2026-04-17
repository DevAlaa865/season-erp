using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.Employee;

namespace BranchERP.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse<IReadOnlyList<EmployeeDto>>> GetAllAsync();

        Task<ApiResponse<IReadOnlyList<EmployeeDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null);

        Task<ApiResponse<EmployeeDto>> GetByIdAsync(int id);

        Task<ApiResponse<EmployeeDto>> CreateAsync(EmployeeCreateUpdateDto model);

        Task<ApiResponse<EmployeeDto>> UpdateAsync(int id, EmployeeCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
