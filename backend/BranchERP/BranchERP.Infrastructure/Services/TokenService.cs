using BranchERP.Infrastructure.Configuration;
using BranchERP.Infrastructure.Data;
using BranchERP.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BranchERP.Infrastructure.Services
{
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly PermissionService _permissionService;
        private readonly AppDbContext _context;

        public TokenService(
            IOptions<JwtSettings> jwtSettings,
            PermissionService permissionService,
            AppDbContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _permissionService = permissionService;
            _context = context;
        }

        public async Task<string> GenerateToken(ApplicationUser user, IList<string> roles)
        {
            string branchName = "";
            string cityName = "";

            if (user.BranchId.HasValue)
            {
                var branch = await _context.Branches
                    .Include(b => b.City)
                    .FirstOrDefaultAsync(b => b.Id == user.BranchId.Value);

                if (branch != null)
                {
                    branchName = branch.BranchName;
                    cityName = branch.City.CityName;
                }
            }
            else if (user.CityId.HasValue)
            {
                var city = await _context.Cities
                    .FirstOrDefaultAsync(c => c.Id == user.CityId.Value);

                if (city != null)
                    cityName = city.CityName;
            }

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),

        new Claim("userType", user.UserType.ToString()),

        new Claim("branchId", user.BranchId?.ToString() ?? "0",ClaimValueTypes.Integer),
        new Claim("branchName", branchName),

        new Claim("cityId", user.CityId?.ToString() ?? ""),
        new Claim("cityName", cityName),

        new Claim("employeeId", user.EmployeeId?.ToString() ?? ""),
        new Claim("departmentId", user.DepartmentId?.ToString() ?? "")
    };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);
            foreach (var perm in permissions)
                claims.Add(new Claim("permission", perm));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
