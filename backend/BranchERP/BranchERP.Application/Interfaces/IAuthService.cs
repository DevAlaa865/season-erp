using BranchERP.Application.DTOs.Auth;
using BranchERP.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchERP.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> LoginAsync(LoginDto model);
        Task<ApiResponse<string>> RegisterAsync(RegisterDto model);
        Task<ApiResponse<string>> AdminResetPasswordAsync(AdminResetPasswordDto model);
    }
}
