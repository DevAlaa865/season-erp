using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.Country;

namespace BranchERP.Application.Interfaces
{
    public interface ICountryService
    {
        Task<ApiResponse<IReadOnlyList<CountryDto>>> GetAllAsync();

        Task<ApiResponse<IReadOnlyList<CountryDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null);

        Task<ApiResponse<CountryDto>> GetByIdAsync(int id);

        Task<ApiResponse<CountryDto>> CreateAsync(CountryCreateUpdateDto model);

        Task<ApiResponse<CountryDto>> UpdateAsync(int id, CountryCreateUpdateDto model);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
