using Microsoft.AspNetCore.Mvc;
using BranchERP.Application.DTOs.BranchDailyTarget;
using BranchERP.Application.Interfaces;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchDailyTargetController : ControllerBase
    {
        private readonly IBranchDailyTargetService _service;

        public BranchDailyTargetController(IBranchDailyTargetService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpGet("by-branch-date")]
        public async Task<IActionResult> GetByBranchAndDate(int branchId, DateTime date)
            => Ok(await _service.GetByBranchAndDateAsync(branchId, date));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchDailyTargetHeaderCreateUpdateDto model)
            => Ok(await _service.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] BranchDailyTargetHeaderCreateUpdateDto model)
            => Ok(await _service.UpdateAsync(id, model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteAsync(id));
    }
}
