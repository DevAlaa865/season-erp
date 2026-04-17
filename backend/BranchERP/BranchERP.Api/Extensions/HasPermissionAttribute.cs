using Microsoft.AspNetCore.Authorization;

namespace BranchERP.Api.Extensions
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
