using BranchERP.Application.DTOs.ShortageType;
using BranchERP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BranchERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortageTypeController : ControllerBase
    {
        private readonly IShortageTypeService _service;

        public ShortageTypeController(IShortageTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int pageIndex = 1, int pageSize = 10, string? search = null)
            => Ok(await _service.GetPagedAsync(pageIndex, pageSize, search));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShortageTypeCreateUpdateDto model)
            => Ok(await _service.CreateAsync(model));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ShortageTypeCreateUpdateDto model)
            => Ok(await _service.UpdateAsync(id, model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteAsync(id));
    }
}
