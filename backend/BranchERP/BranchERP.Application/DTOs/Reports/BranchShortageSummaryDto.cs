using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchERP.Application.DTOs.Reports
{
    public class BranchShortageSummaryDto
    {
        public int ShortageTypeId { get; set; }
        public string ShortageTypeName { get; set; }   // مثلاً: خصم، مشتريات، مكافأة، فرق جرد...
        public decimal Amount { get; set; }
    }
}
