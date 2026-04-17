using BranchERP.Application.DTOs;
using BranchERP.Application.DTOs.PermissionDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BranchERP.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<List<string>> GetUserPermissionsAsync(string userId);

        Task<List<PermissionDto>> GetAllAsync();
        Task<PermissionDto> CreateAsync(PermissionCreateDto model);
        Task<PermissionDto?> UpdateAsync(int id, PermissionCreateDto model);
        Task<bool> DeleteAsync(int id);
    }
}
