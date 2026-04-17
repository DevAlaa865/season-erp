using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchERP.Application.DTOs.Auth
{
  
        public class UpdateUserDataDto
        {
            public string? Id { get; set; }
            public string? DisplayName { get; set; }
            public string? Email { get; set; }
            public bool IsActive { get; set; }

        public int UserType { get; set; }   // 1 = Branch, 2 = CityManager, 3 = Central
        public string? UserName { get; set; }   // 🔥 جديد
        public int? BranchId { get; set; }     // 🔥 جديد
    }
    
}
