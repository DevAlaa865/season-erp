using BranchERP.Application.DTOs.Region;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _regionService.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            int pageIndex = 1,
            int pageSize = 10,
            string? search = null)
            => Ok(await _regionService.GetPagedAsync(pageIndex, pageSize, search));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _regionService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegionCreateUpdateDto model)
            => Ok(await _regionService.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegionCreateUpdateDto model)
            => Ok(await _regionService.UpdateAsync(id, model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _regionService.DeleteAsync(id));
    }
}
